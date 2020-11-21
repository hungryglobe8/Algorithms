"""
This module can be used to create:
	input files - extracts all roots from a devset folder (easily make correct input files)
	response files - necessary in scoring QA system
"""
import os

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
			# Leave a line between each answer.
			f.write(f"{str(answer)}\n")

def make_input_file(folder_name):
	"""
	Write an input file given a directory containing stories. 

	Creates the input file in folder_name + '.input'.
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
	new_file_name = folder_name + ".input"
	with open(new_file_name, 'w') as f:
		# Remove leading '.'.
		f.write(folder_name.lstrip('.'))
		for story_id in story_ids:
			f.write(f"\n{story_id}")