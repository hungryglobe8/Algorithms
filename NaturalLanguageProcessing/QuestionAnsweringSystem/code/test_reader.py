import pytest, reader, qa_io, os
test_folder = "./tests/smalldevset/"

def test_read_questions():
	question_file = test_folder + "developset-v2/1999-W02-5"

	questions = reader.read_questions(question_file)

	assert len(questions) == 6

def test_read_input_file():
	input_file = test_folder + "single.input"

	lines = reader.read_from_file(input_file)

	assert len(lines) == 3

def test_write_response():
	response_file = test_folder + "basic_answers"
	answers = [qa_io.Answer(1, "500"), qa_io.Answer(2, "750"), qa_io.Answer(3, "1000")]

	reader.write_response_file(response_file, answers)
	
	assert os.path.exists(response_file + ".response")

def test_read_questions_write_responses():
	file_name = test_folder + "smalldevset/1999-W02-5"
	questions = reader.read_questions(file_name)
	answers = [question.answer_question() for question in questions]

	reader.write_response_file(file_name, answers)

	assert os.path.exists(file_name + ".response")