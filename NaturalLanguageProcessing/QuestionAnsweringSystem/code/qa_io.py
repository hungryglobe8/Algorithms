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
		# Figure out type of question.
		for word in self.question:
			word = word.lower()
			if word in self.question_words:
				self.type = word
				return

	def answer_question(self):
		''' Returns the answer to a question. '''
		return Answer(self.question_id, None)

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
