'''
Read stories from a story file.
Save relevant information such as HEADLINE, DATE, and STORYID.

returns an object containing these and text seperated by paragraphs.

Can ask for most likely paragraph to contain text (such as answer to a question).
'''
class Story():
	'''
	Contains all relevant information of a story.
	'''
	def __init__(self, file_name):
		''' 
		Read story information from a file.
		'''
		sentences = []
		with open(file_name) as f:
			self.headline = f.readline().split()[1:]
			self.date = f.readline().split()[1:]
			self.storyid = f.readline().split()[1:]
			while(False):
				pass
				# line = f.readline()
				# # Last sentence complete.
				# if not line:
				# 	return sentences

				# # Sentence has words.
				# if line != []:
				# 	# Might not work
				# 	if class_name is not None:
				# 		sentences.append(class_name(line))
				# 	else:
				# 		sentences.append(line.strip())

def read_story(file_name):
	return Story(file_name)