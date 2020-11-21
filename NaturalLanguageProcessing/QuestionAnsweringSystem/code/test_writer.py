import pytest, writer, os
test_folder = "./tests/"

def get_root(name):
	return test_folder + name

def test_small_dev_set():
	root = get_root("smalldevset")
	file_name = root + ".input"

	writer.make_input_file(root)

	assert os.path.getsize(file_name) == 43

def test_large_dev_set():
	root = get_root("developset-v2")
	file_name = root + ".input"

	writer.make_input_file(root)

	assert os.path.getsize(file_name) == 897

def test_midpoint_dev_set():
	root = get_root("testset1")
	file_name = root + ".input"

	writer.make_input_file(root)

	assert os.path.getsize(file_name) == 484

class TestModifiedAnswerFile():
	def test_modified_answer_where(self):
		root = get_root("smalldevset")

		writer.modified_answers("where", root)

		assert os.path.exists(root + "-where.answers")

	def test_modified_answer_who(self):
		root = get_root("testset1")

		writer.modified_answers("who", root)

		assert os.path.exists(root + "-who.answers")

def test_score_subset_small():
	file_name = test_folder + "smalldevset.input"

	writer.score_subset(file_name, "who")

	assert True

import qa_io, reader
class TestResponseFile():
	def test_write_response(self):
		file_name = test_folder + "basic_answers"
		answers = [qa_io.Answer(1, "500"), qa_io.Answer(2, "750"), qa_io.Answer(3, "1000")]

		writer.response_file(file_name, answers)
		
		assert os.path.exists(file_name + ".response")

	def test_read_questions_write_responses(self):
		file_name = test_folder + "smalldevset/1999-W02-5"
		questions = reader.read_questions(file_name)
		answers = [question.answer_question() for question in questions]

		writer.response_file(file_name, answers)

		assert os.path.exists(file_name + ".response")