import pytest
import reader, text, qa_io
test_folder = "./tests/smalldevset/"

class TestQuestions():
	def make_basic_question(self, q_id="1", question=["Who", "am", "I?"], difficulty="Easy"):
		return qa_io.make_question(q_id, question, difficulty)

	def test_make_basic_question_not_null(self):
		assert self.make_basic_question() is not None

	def test_basic_question_correct_params(self):
		sut = self.make_basic_question()

		assert sut.question_id == "1"
		assert len(sut.question) == 3
		assert sut.difficulty == "Easy"

	def test_basic_question_correct_type(self):
		question = "Where did you go?".split()
		sut = self.make_basic_question(question=question)

		assert isinstance(sut, qa_io.WhereQuestion)

	def test_answer_basic_question(self):
		sut = self.make_basic_question()

		res = sut.answer_question()

		assert isinstance(res, qa_io.Answer)

class TestAnswers():
	def make_basic_answer(self, a_id="1", answer="Jeremy"):
		return qa_io.Answer(a_id, answer)

	def test_make_basic_answer_not_null(self):
		assert self.make_basic_answer() is not None

	def test_basic_answer_correct_fields(self):
		sut = self.make_basic_answer()

		assert sut.answer_id == "1"
		assert sut.answer == "Jeremy"

	def test_basic_answer_to_string(self):
		sut = self.make_basic_answer()

		assert str(sut) == "QuestionID: 1\nAnswer: Jeremy\n"

class TestRealQuestions():
	# Save roots for use in specific tests.
	ns_root = test_folder + "1999-W02-5"

	def read_story(self, file_root):
		questions = reader.read_questions(file_root)
		story = text.read_story(file_root)
		return questions, story

	def test_where_location(self):
		questions, story = self.read_story(self.ns_root)
		res = questions[0].answer_question(story)

		assert res.answer == "Liverpool"

	def test_who_principal(self):
		questions, story = self.read_story(self.ns_root)
		res = questions[1].answer_question(story)

		assert res.answer == "Betty Jean Aucoin"

	def test_what_metal(self):
		questions, story = self.read_story(self.ns_root)
		res = questions[2].answer_question(story)

		assert res.answer == "turned it into a fitness club"		