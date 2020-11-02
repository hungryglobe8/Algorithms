import pytest
from qa_io import *
test_folder = "./NaturalLanguageProcessing/QuestionAnsweringSystem/tests/"

class TestQuestions():
	def make_basic_question(self, q_id="1", question="Who am I?", difficulty="Easy"):
		return Question(q_id, question, difficulty)

	def test_make_basic_question_not_null(self):
		assert self.make_basic_question() is not None

	def test_make_basic_question_correct_type(self):
		sut = self.make_basic_question()

		assert sut.type == "who"