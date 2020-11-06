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
		most_likely_sentences = story.get_most_likely_sentences(question, include=include)
		# Get named entities.
		ents = story.doc.ents

		answer = "NotImplemented - " + " ".join(question)
		# Start with most likely paragraph, look for a match.
		for sentence, likelihood in most_likely_sentences:
			# Get entities of each sentence.
			sent_ents = sp(' '.join(sentence.text)).ents
			# Add possible answers to list.
			for ent in sent_ents:
				if ent.label_ in acceptable_types:
					# poss.append(ent.text)
					return Answer(self.question_id, ent.text)

		return Answer(self.question_id, answer)

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

	def __str__(self):
		''' Leaves self.answer if the value is None. '''			
		return f"QuestionID: {self.answer_id}\nAnswer: {self.answer}\n"

def make_question(question_id, question, difficulty):
	params = question_id, question, difficulty
	switcher = {
		"where": WhereQuestion(*params),
		"who": WhoQuestion(*params)
	}
	# Figure out type of question.
	for word in question:
		word = word.lower()
		if word in Question.question_words:
			try:
				return switcher[word]
			except:
				pass
	print (question)
	return Question(*params)
	#raise NotImplementedError(f"Question word not in {question}")