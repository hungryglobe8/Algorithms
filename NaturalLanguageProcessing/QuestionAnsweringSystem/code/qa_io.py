"""
This file maintains various types of classes in use by a QA system:
	Questions
	Answers
"""
class Question():
	"""
	QuestionID
	Question
	Difficulty
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