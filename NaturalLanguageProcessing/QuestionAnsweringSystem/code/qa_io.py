"""
This file maintains various types of classes in use by a QA system:
	Questions
	Answers
"""
import spacy
sp = spacy.load('en_core_web_sm')

class Question():
	"""
	QuestionID - string
	Question - list of strings (original format)
	Difficulty - string
	Type - first question word encountered in sentence (None if not discovered)
	"""
	# Possibly parse these before
	question_words = ["who", "what", "when", "where", "why", "how much", "how"]
	secondary_questions = ["if"]
	def __init__(self, question_id, question, difficulty):
		self.question_id = question_id
		self.question = question
		self.difficulty = difficulty

	def answer_question(self, story, acceptable_types, question, include=[], exclude=[]):
		""" Should be implemented in subclasses. """
		# Get most likely sentences (sorted by value).
		most_likely_sentences = story.get_most_likely_sentences(question, include=include, exclude=exclude)
		# Get named entities.
		ents = story.doc.ents

		answer = "NotImplemented - " + " ".join(question)
		# Start with most likely paragraph, look for a match.
		for sentence, likelihood in most_likely_sentences:
			# Get entities of each sentence.
			sent_ents = sp(' '.join(sentence.text)).ents
			# Add possible answers to list.
			for ent in sent_ents:
				if ent.label_ in acceptable_types and not any(word in ent.text.split() for word in question):
					# poss.append(ent.text)
					print(ent.text, ent.label_)
					return Answer(self.question_id, ent.text)

		return Answer(self.question_id, answer)

class HowQuestion(Question):
	"""

	"""
	acceptable_types = ["PERSON"]
	quantifiers = {"big":["QUANTITY"], "much":["MONEY", "QUANTITY", "COUNT"], "tall":["QUANTITY"], "many":["CARDINAL", "QUANTITY", "COUNT"]}
	def __init__(self, question_id, question, difficulty):
		Question.__init__(self, question_id, question, difficulty)

	def answer_question(self, story):
		# Get type of how question to find acceptable final answer types.
		how_type = ""
		for i, word in enumerate(self.question):
			if word.lower() == "how":
				how_type = self.question[i+1]
				break

		if how_type not in self.quantifiers.keys():
			most_likely_sentences = story.get_most_likely_sentences(self.question)
			return Answer(self.question_id, most_likely_sentences[0][0].text)

		# Currently replacing '-' with ' ' in Answer class itself.
		answer = super().answer_question(story, HowQuestion.quantifiers[how_type], self.question)
		if how_type == "big":
			answer.replace('-', ' ')
		elif how_type == "much" and answer.answer.isdigit():
			answer.prepend('$')
		return answer

class WhatQuestion(Question):
	"""	No such thing as acceptable types for this one.	"""
	acceptable_types = ["FAC", "PERSON", "ORG"]

	def __init__(self, question_id, question, difficulty):
		Question.__init__(self, question_id, question, difficulty)

	def answer_question(self, story):
		"""
		Not extremely accurate (2nd most likely)
		Get rid of pronouns?
		"""
		# Get most likely sentences (sorted by value).
		most_likely_sentences = story.get_most_likely_sentences(self.question, exclude=self.pronouns())
		question_type = ""
		question_doc = sp(' '.join(self.question))
		for i, token in enumerate(question_doc):
			#Similarities include token.pos_ as NOUN or token.tag_ as NN
			if token.text.lower() == "what":
				question_type = question_doc[i+1].pos_
				break

		if question_type is "NOUN":
			return super().answer_question(story, self.acceptable_types, self.question)

		return Answer(self.question_id, most_likely_sentences[0][0].text)

	def pronouns(self):
		res = []
		for word in self.question:
			if word[0].isupper():
				res.append(word.lower())
		return res

class WhenQuestion(Question):
	acceptable_types = ["DATE"]
	def __init__(self, question_id, question, difficulty):
		Question.__init__(self, question_id, question, difficulty)

	def answer_question(self, story):
		return super().answer_question(story, WhenQuestion.acceptable_types, self.question)

class WhoQuestion(Question):
	"""

	"""
	acceptable_types = ["PERSON"]
	def __init__(self, question_id, question, difficulty):
		Question.__init__(self, question_id, question, difficulty)

	def answer_question(self, story):
		"""
		Not extremely accurate (2nd most likely)
		Get rid of pronouns?
		"""
		return super().answer_question(story, WhoQuestion.acceptable_types, self.question)

class WhereQuestion(Question):
	extra_words = ["in"]
	acceptable_types = ["GPE", "PERSON"]

	def __init__(self, question_id, question, difficulty):
		Question.__init__(self, question_id, question, difficulty)

	def answer_question(self, story):
		return super().answer_question(story, WhereQuestion.acceptable_types, WhereQuestion.extra_words, include=WhereQuestion.extra_words)

	def break_ties(self, poss):
		''' Loop through acceptable types and return best possibility. '''
		for best in self.acceptable_types:
			for answer in poss:
				if answer.label_ == best:
					return answer

class Answer():
	"""
	Answers for questions consist of the ID and the answer itself.

	String verison is printed as:
		ID
		Answer
	"""
	def __init__(self, answer_id, answer):
		self.answer_id = answer_id
		self.answer = answer

	def replace(self, remove, add):
		self.answer = self.answer.replace(remove, add)

	def prepend(self, add):
		self.answer = add + self.answer

	def __str__(self):
		''' Leaves self.answer if the value is None. '''			
		return f"QuestionID: {self.answer_id}\nAnswer: {self.answer}\n"

def make_question(question_id, question, difficulty):
	params = question_id, question, difficulty
	switcher = {
		"where": WhereQuestion(*params),
		"who": WhoQuestion(*params),
		"what": WhatQuestion(*params),
		"how": HowQuestion(*params),
		"when": WhenQuestion(*params)
	}
	# Figure out type of question.
	for word in question:
		word = word.lower()
		if word in Question.question_words:
			try:
				return switcher[word]
			except:
				pass
	return Question(*params)
	#raise NotImplementedError(f"Question word not in {question}")