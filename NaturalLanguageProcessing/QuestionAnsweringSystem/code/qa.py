'''
First line of input file is directory path.

Remaining lines are stories and questions to solve.
Produce an answer file for each.
'''
import sys, os, re, math, story
import validate, reader

def parse_file(file_name):
	''' 
	Divides lines of a text file into:
		directory - first line of file should be where stories and questions are located prepended with "."
		story ids - remaining lines of file should be story and corresponding question ids
	'''
	lines = reader.read_from_file(file_name)
	return "." + lines[0], lines[1:]

def formulate_response(root):
	'''
	Read story at root.story (happens in reader).
	Read questions at root.questions (happens in reader).
	Answers questions based on story text and type of question.
	'''
	text = story.Story(root)
	questions = reader.read_questions(root)
	# Answer all questions. TODO pass in text to answer_question
	answers = [question.answer_question(text) for question in questions]
	# Write answers.
	reader.write_response_file(root, answers)

def main(args):
	# Check input file exists.
	validate.validate_file_names(args)
	# Get lines of input file.
	directory, story_ids = parse_file(args[1])

	for storyID in story_ids:
		# Root is used to access .story and .questions
		root = directory + storyID
		formulate_response(root)

if (__name__ == "__main__"):
	main(sys.argv)