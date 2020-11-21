'''
First line of input file is directory path.

Remaining lines are stories and questions to solve.
Produce an answer file for each.
'''
import spacy
sp = spacy.load('en_core_web_sm')
import sys, os, re, math, text
import validate, reader, writer

def parse_file(file_name):
	''' 
	Divides lines of a text file into:
		directory - first line of file should be where stories and questions are located prepended with "."
		story ids - remaining lines of file should be story and corresponding question ids
	'''
	lines = reader.read_from_file(file_name)
	return "." + lines[0], lines[1:]

def formulate_response(root, question_type=None):
	'''
	Read story at root.story (happens in reader).
	Read questions at root.questions (happens in reader).
	Answers questions based on story text and type of question.
	'''
	story = text.Story(root)
	questions = reader.read_questions(root)
	# Answer all questions. TODO pass in text to answer_question
	answers = []
	for question in questions:
		answer = None
		if (question_type is None or any(word.lower() == question_type for word in question.question)):
			try:
				answer = question.answer_question(story)
			except:
				# No answer
				answer = question.blank_answer()
			answers.append(answer)

	# Write answers.
	if question_type is not None:
		writer.response_file(root + f"-{question_type}", answers)
	else:
		writer.response_file(root, answers)

def main(args, question_type=None):
	# Check input file exists.
	validate.validate_file_names(args)
	# Get lines of input file.
	directory, story_ids = parse_file(args[1])

	for storyID in story_ids:
		# Root is used to access .story and .questions
		root = directory + storyID
		formulate_response(root, question_type)

if (__name__ == "__main__"):
	main(sys.argv)