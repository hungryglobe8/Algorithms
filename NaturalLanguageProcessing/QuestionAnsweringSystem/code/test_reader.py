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