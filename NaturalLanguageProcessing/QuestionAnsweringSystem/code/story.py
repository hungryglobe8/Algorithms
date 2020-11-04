'''
Read stories from a story file.
Save relevant information such as HEADLINE, DATE, and STORYID.

returns an object containing these and text seperated by paragraphs.

Can ask for most likely paragraph to contain text (such as answer to a question).
'''
import reader
stopwords_file = "./NaturalLanguageProcessing/QuestionAnsweringSystem/code/stopwords.txt"
stopwords = reader.read_from_file(stopwords_file)

class Story():
	'''
	Contains all relevant information of a story.

	Leave off .story part of file_name.
	'''
	def __init__(self, file_name):
		''' 
		Read story information from a file (leave off .story).
			Headline, Date - List of strings
			StoryID - String
			Text - List of Paragraphs
		'''
		with open(file_name + ".story") as f:
			self.headline = f.readline().split()[1:]
			self.date = f.readline().split()[1:]
			self.storyid = f.readline().split()[1]
			# Skip three lines.
			for _ in range(3):
				f.readline()
			# Get paragraphs.
			self.paragraphs = self.split_into_paragraphs(f.readlines())

	def split_into_paragraphs(self, text):
		''' 
		Splits a list of sentences into paragraph sections. 
		If a line is a newline char by itself, start a new paragraph. 
		'''
		paragraphs = []
		paragraph = Paragraph()
		for line in text:
			# If line is empty, start a new paragraph.
			if line == "\n":
				paragraphs.append(paragraph)
				paragraph = Paragraph()
			# Line has some content besides newline char.
			else:
				paragraph.add_words(line)

		# Add last paragraph.
		paragraphs.append(paragraph)
		
		return paragraphs

class Paragraph():
	"""
	A paragraph is a section of text with related content.
	Can be searched for answers.
	"""
	def __init__(self):
		self.text = []

	def add_words(self, line):
		"""
		Adds words to the list already maintained by the paragraph.
		"""
		self.text += line.split()

	def get_context_window(self, word, k):
		"""
		Find k words surrounding a specified word.
		"""
		if word not in self.text:
			return None

		else:
			pass

	def __len__(self):
		return len(self.text)

class Key_Paragraph(Paragraph):
	"""
	A key paragraph only keeps track of unique words not part of stopwords list.
	"""
	def __init__(self):
		Paragraph.__init__(self)

def read_story(file_name):
	return Story(file_name)