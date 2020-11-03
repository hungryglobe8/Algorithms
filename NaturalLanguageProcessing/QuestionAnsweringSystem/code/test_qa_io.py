import pytest
from qa_io import *
test_folder = "./NaturalLanguageProcessing/QuestionAnsweringSystem/tests/"

class TestQuestions():
	def make_basic_question(self, q_id="1", question="Who am I?", difficulty="Easy"):
		return Question(q_id, question, difficulty)

	def test_make_basic_question_not_null(self):
		assert self.make_basic_question() is not None

	def test_basic_question_correct_params(self):
		sut = self.make_basic_question()

		assert sut.question_id == "1"
		assert len(sut.question) == 3
		assert sut.difficulty == "Easy"

	def test_basic_question_correct_type(self):
		sut = self.make_basic_question()

		assert sut.type == "who"

	def test_answer_basic_question(self):
		sut = self.make_basic_question()

		res = sut.answer_question()

		assert isinstance(res, Answer)

class TestAnswers():
	def make_basic_answer(self, a_id="1", answer="Jeremy"):
		return Answer(a_id, answer)

	def test_make_basic_answer_not_null(self):
		assert self.make_basic_answer() is not None

	def test_basic_answer_correct_fields(self):
		sut = self.make_basic_answer()

		assert sut.answer_id == "1"
		assert sut.answer == "Jeremy"

	def test_basic_answer_to_string(self):
		sut = self.make_basic_answer()

		assert str(sut) == "QuestionID: 1\nAnswer: Jeremy"