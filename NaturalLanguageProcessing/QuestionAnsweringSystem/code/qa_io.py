"""
This file maintains various types of classes in use by a QA system:
	Questions
	Answers
"""
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

	def answer_question(self, answer=None):
		""" Should be implemented in subclasses. """
		if answer is None:
			return Answer(self.question_id, "NotImplemented - " + " ".join(self.question))
		else:
			return Answer(self.question_id, answer)

class WhereQuestion(Question):
	"""
	QuestionID - string
	Question - list of strings (original format)
	Difficulty - string
	Type - first question word encountered in sentence (None if not discovered)
	"""
	extra_words = ["in"]
	acceptable_types = ["GPE", "PERSON"]

	def __init__(self, question_id, question, difficulty):
		Question.__init__(self, question_id, question, difficulty)

	def answer_question(self, story):
		# Get most likely paragraphs (sort by value). TODO consider sentences?
		most_likely_paragraphs = story.get_possible_paragraphs(self.question + self.extra_words)
		# Get named entities.
		ents = story.get_named_entities()

		answer = "no answer found"
		# Start with most likely paragraph, look for a match.
		for paragraph, likelihood in most_likely_paragraphs:
			poss = []
			for ent in list(ents):
				if ent.string in ' '.join(paragraph.text):
					poss.append(ent)

			# No match.
			if len(poss) == 0:
				continue
			# One match.
			elif len(poss) == 1:
				answer = poss[0]
				break
			# Multiple matches.
			else:
				answer = self.break_ties(poss)
				break

		return Question.answer_question(self, answer)

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
		"where": WhereQuestion(*params)
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