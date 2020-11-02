'''


'''
import sys, os, re, math
import validate, reader

def main(args):
	input_file = validate.parse_args(args, 1)
	# Read from files.
	questions = reader.read_questions(input_file)
	# # Run lesk algorithm on each sentence.
	# text = []
	# for sentence in sentences:
	# 	ordered_senses = run_lesk(senses, stopwords, sentence.words)
	# 	text.append(ordered_senses)
	# # Print results to new .lesk file.
	# make_file(test_file, text)
	return questions

if (__name__ == "__main__"):
	main(sys.argv)