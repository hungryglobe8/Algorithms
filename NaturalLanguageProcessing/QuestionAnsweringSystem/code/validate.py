''' 
Contains functions useful for checking the validity of args.
'''
import os

def parse_args(args, length):
	''' Check validity and return args in necessary formats. '''
	validate_len_args(args, length)
	validate_file_names(args)
	return args[1:]

def validate_len_args(args, length):
	''' Check if there are length arguments following the program. '''
	if len(args) != length + 1:
		raise ValueError("There should be one argument following your program.")

def validate_file_names(args):
	'''
	Check that files exist, from the current directory.

	Don't forget to cd to correct directory!
	'''
	for arg in args[1:]:
		if not os.path.isfile(arg):
			raise ValueError(f"{arg} is not a valid file.")


