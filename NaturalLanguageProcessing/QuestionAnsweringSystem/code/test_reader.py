import pytest, reader
test_folder = "./NaturalLanguageProcessing/QuestionAnsweringSystem/tests/"

def test_read_questions():
	question_file = test_folder + "developset-v2/1999-W02-5.questions"

	questions = reader.read_questions(question_file)

	assert len(questions) == 6