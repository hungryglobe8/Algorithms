import pytest, qa, os
test_folder = "./NaturalLanguageProcessing/QuestionAnsweringSystem/tests/"

def single_input_args():
	input_file = test_folder + "single.input"

	return [None, input_file]

def test_single_input_main():
	args = single_input_args()

	qa.main(args)

	assert os.path.exists(test_folder + "smalldevset/1999-W03-5.response")
