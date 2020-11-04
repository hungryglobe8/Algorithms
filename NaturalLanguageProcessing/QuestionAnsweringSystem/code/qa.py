'''
First line of input file is directory path.

Remaining lines are stories and questions to solve.
Produce an answer file for each.
'''
import sys, os, re, math, story
import validate, reader

def main(args):
	# Check input file exists.
	validate.validate_file_names(args)
	# Get lines of input file.
	lines = reader.read_from_file(args[1])
	directory = lines[0]
	storyIDs = lines[1:]
	# For each id, read story and questions.
	for storyID in storyIDs:
		# Root is used to access .story and .questions
		root = directory + storyID
		story = story.Story(root)
		questions = reader.read_questions(root)
		# Answer all questions. TODO pass in story to answer_question
		answers = [question.answer_question() for question in questions]
		# Write answers.
		reader.write_response_file(root, answers)

if (__name__ == "__main__"):
	main(sys.argv)