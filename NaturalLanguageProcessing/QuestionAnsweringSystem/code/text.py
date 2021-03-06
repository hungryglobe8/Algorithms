'''
Parse stories apart into their important chunks for analysis.
Stories -
	Save relevant information such as HEADLINE, DATE, and STORYID.
	Save text, sentences, and paragraphs.
	Make a vocabulary for terms in the story.
	Query for most likely sentence by cosine similarity (if None, use paragraph).
	Use external library to get named ents and pos.

Paragraphs -

Sentences -
	Make a vector based on story vocab (include or exclude extra terms).
'''
import reader, string
stopwords_file = "./code/stopwords.txt"
stopwords = reader.read_from_file(stopwords_file)
import qa_io
sp = qa_io.sp

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
			
			# Get text.
			self.text = f.readlines()
			# Save spacy info.
			self.doc = sp(''.join(self.text))
			# Get sentences and paragraphs.
			self.sentences = list(self.doc.sents)
			self.paragraphs = ''.join(self.text).split('\n\n')

		# Vocab set is lowercase, stripped of punctuation, unique (possible stems?)
		self.vocab = get_vocab_from_doc([token.text for token in self.doc])

	def get_most_likely_sentences(self, question, include=[], exclude=[]):
		# exclude stopwords and user-defined words.
		sentence_vecs = [SignatureVector(sentence.text.split(), self.vocab, include, stopwords + exclude) for sentence in self.sentences]
		#TODO QUESTION.words
		question_vec = SignatureVector(question, self.vocab, include, stopwords + exclude)

		scores = dict()
		for sentence_vec in sentence_vecs:
			scores[sentence_vec] = sentence_vec.compare(question_vec)

		res = sorted(scores.items(), key=lambda x: x[1], reverse=True)
		#self.debug(res)
		return res

	def debug(self, res):
		for sig_vec, score in res[:6]: print(f"{sig_vec.text}: {score}")

class SignatureVector():
	''' Signature vector for a sense. 
	Excludes stopwords by default. '''
	def __init__(self, text, vocab, include=[], exclude=[]):
		self.text = text
		text_set = get_vocab_from_doc(text, include=include, exclude=exclude)
		self.vector = {word: 0 for word in vocab}
		for word in text_set:
			if word in vocab:
				self.vector[word] = 1

	def compare(self, other):
		''' Compare the cosine similarity between two Signature Vectors. '''
		x_vec = list(self.vector.values())
		y_vec = list(other.vector.values())

		return self.cosine_sim(x_vec, y_vec)

	@staticmethod
	def cosine_sim(x_vec, y_vec):
		''' Calculates the cosine similarity between two numeric lists. Returns 0 if the denominator is 0. '''
		# Calculate numerator.
		num = 0
		for i in range(len(x_vec)):
			num += (x_vec[i] * y_vec[i])

		# Calculate denominator.
		denom = 0
		x_den = sum(x**2 for x in x_vec) ** .5
		y_den = sum(y**2 for y in y_vec) ** .5
		denom = x_den * y_den

		# Don't allow for division by 0.
		if denom == 0:
			return 0
		else:
			return round(num/denom, 2)

def get_vocab_from_list(words, include=[], exclude=[]): 
	""" Get spacy information from a list of words before returning vocab. """
	return get_vocab_from_doc(sp(' '.join(words)), include, exclude)

def get_vocab_from_doc(doc, include=[], exclude=[]):
	"""
	Get vocab returns a unique list of the lowercase, punctuation-stripped versions of these words.
	TODO - lemmatized of words?

	Doc must be sp document.
	Include and Exclude can be used to remove things like stopwords or include specific additional terms.
	"""
	unique = []
	for word in doc:
		#word = word.lemma_
		word = word.lower().translate(str.maketrans('', '', string.punctuation))
		if word not in unique:
			# Add a word if it is in include (highest priority) or if it is not in exclude.
			if word in include or word not in exclude:
				unique.append(word)
	return unique

def read_story(file_name):
	return Story(file_name)