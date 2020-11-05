import pytest, qa, os
test_folder = "./NaturalLanguageProcessing/QuestionAnsweringSystem/tests/"
input_file = test_folder + "single.input"

def single_input_args():
	input_file = test_folder + "single.input"

	return [None, input_file]

def test_parse_file():
	directory, story_ids = qa.parse_file(input_file)

	assert os.path.exists(directory)
	assert len(story_ids) == 2

def test_write_single_response():
	root = test_folder + "smalldevset/1999-W02-5"

	qa.formulate_response(root)

	assert os.path.exists(root + ".response")

def test_single_input_main():
	args = single_input_args()

	qa.main(args)

	assert os.path.exists(test_folder + "smalldevset/1999-W02-5.response")
	assert os.path.exists(test_folder + "smalldevset/1999-W03-5.response")