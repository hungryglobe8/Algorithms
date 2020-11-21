''' Read strings from data into various formats. '''
import qa_io
def read_from_file(file_name, class_name=None):
    ''' 
    Read something from a file. Scans by new lines and class_name specifies how to parse the line.
    If class_name is not specified, return a straight list of the file seperated by newlines.
    '''
    sentences = []
    with open(file_name) as f: 
        while(True):
            line = f.readline()
            # Last sentence complete.
            if not line:
                return sentences

            # Sentence has words.
            if line != []:
                if class_name is not None:
                    sentences.append(class_name(line))
                else:
                    sentences.append(line.strip())

def read_questions(file_name):
	''' 
	Read questions from a file. Questions are formed of three lines (id, question, and difficulty)
	and separated by newline chars.

	Appends .questions to the name.
	'''
	questions = []
	with open(file_name + ".questions") as f: 
		line = f.readline()
		# Read until there are no more groups.
		while(line):
			# Get question id, question, and difficulty.
			q_id = line.split()[1]
			question = f.readline().split()[1:]
			difficulty = f.readline().split()[1]
			# Add question to list.
			questions.append(qa_io.make_question(q_id, question, difficulty))
			# Move to next
			f.readline()
			line = f.readline()
		
	return questions