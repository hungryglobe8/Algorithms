"""
This module can be used to create:
	Input files - extract all story roots from a devset folder (easily make correct input files)
	Response files - necessary in scoring QA system
	Std output - probably best redirected to a .response file
	Modified answer files - grab answers of a specific question type 
"""
import qa, os, sys

def response_file(old_file_name, answers):
	"""
	Write a response file given a list of answers (qa_io.Answer) and a file name. 

	Appends '.response' to old_file_name.
	Formatting:
		QuestionID: id
		Answer: answer
		newline
	"""

	new_file_name = old_file_name + ".response"
	with open(new_file_name, 'w') as f:
		for answer in answers:
			f.write(f"{str(answer)}\n")

def std_out_response(answers):
	"""
	Print a list of answers to stdout.

	Can easily send to a file through > response.txt
	Formatting:
		QuestionID: id
		Answer: answer
		newline
	"""

	for answer in answers:
		print(str(answer))

def make_input_file(folder_name):
	"""
	Write an input file given a directory containing stories. 

	Creates the input file as folder_name + '.input'.
	Search the directory of folder_name, looking for files that end with '.story'.
	Write the base names of these files to the input file.
		e.g. './tests/testset1/1999-W01-1.story' -> '1999-W01-1'
	File format:
		folder_name (no leading period)
		base name 1
		base name 2
		...
	"""

	# Get story_ids to add to file.
	story_ids = set()
	for entry in os.scandir(folder_name):
		# Only add if suffix is '.story'.
		# Assumes one story per development folder.
		base, suffix = os.path.basename(entry).split('.')
		if suffix == 'story':
			story_ids.add(base)
	story_ids = sorted(story_ids)

	# Write file.
	new_file_name = folder_name + '.input'
	with open(new_file_name, 'w') as f:
		# Remove leading '.', add '/'
		f.write(folder_name.lstrip('.') + '/')
		for story_id in story_ids:
			f.write(f"\n{story_id}")

def modified_answers(question_type, folder_name):
	"""
	Print specific answers from a directory containing answers. 

	Suggested redirect > '{name}-{question_type}.answers'.
	Search the directory of folder_name, looking for files that end with '.answers'.
	Print answers that match the question_type:
		'where' matches with ->
			QuestionID: 1999-W02-5-1
			Question: Where is South Queens Junior High School located?
			Answer: Liverpool, Nova Scotia | Canada
			Difficulty: Moderate
	Stdout format:
		QuestionID: id
		Question: question
		Answer: answers
		Difficulty: difficulty
		newline
		...
	"""

	# Scan directory for appropriate answers.
	for entry in os.scandir(folder_name):
		# Found answer file.
		if entry.name.endswith('.answers'):
			# Read it.
			with open(entry) as r: 
				while(True):
					# Read 5 lines at a time.
					answer = [r.readline() for x in range(5)]

					# Not enough lines in file.
					if answer[0] == '':
						break

					question = answer[1]
					# Look for matching question word.
					if any(word.lower() == question_type for word in question.split()):
						print(''.join(answer[:-1]))

def main(args):
	"""
	Main can be used to generate modified answer files from the command line.

	These files only contain answers of a specific question type.
	"""
	# Get relevant args.
	folder_name = args[1]
	question_type = args[2]

	modified_answers(question_type, folder_name)

if (__name__ == "__main__"):
	main(sys.argv)