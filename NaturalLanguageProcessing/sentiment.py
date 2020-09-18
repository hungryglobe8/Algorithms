'''
Contains the code to train a machine learning system for
Sentiment classification. This program accepts sentences from movie reviews
as input and labels each sentence as Positive or Negative.
'''
import sys
import os
train_file = ""
test_file = ""
features_file = ""
k = 0

def validate_len_args(args):
    ''' 
    Format to run sentiment is as follows:
    python <path to sentiment.py> <train file> <test file> <features file> <k>
    There should be 5 arguments total in the command line.
    '''
    if len(args) != 5:
        raise ValueError("There should be four arguments following your program.")
    
def validate_file_names(args):
    '''
    Check that files exist, from the current directory.
    
    Don't forget to cd to NaturalLanguageProcessing!
    '''
    for arg in args[1:-1]:
        print(arg)
        if not os.path.isfile(arg):
            raise ValueError(f"{arg} is not a valid file.")

class Record(object):
    """docstring for Record"""
    def __init__(self, FNAME, SNAME, M1, M2, M3, M4):
        super(Record, self).__init__()
        self.FNAME = FNAME
        self.SNAME = SNAME
        self.M1 = M1
        self.M2 = M2
        self.M3 = M3
        self.M4 = M4

class Sentence():
    def __init__(self, label):
        '''
        1 represents a positive label.
        0 represents a negative label.
        '''
        if not label in [0, 1]:
            raise ValueError("Label is incorrectly assigned.")
        else:
            self.label = label
            self.words = []

    def add_word(self, word):
        '''
        Add a new word to the representation of a sentence.
        '''
        self.words.append(word)
    
        
def read_sentences_from_file(file_name):
    '''
    Save training and test files as class labels.
    '''
    with open(file_name) as f: 
        sentences = []
        while(True):
            label = int(f.readline().strip())
            if isinstance(label, int):
                sentence = Sentence(label)
            # Loop until hitting a new line by itself.
            while(sentence):
                word = f.readline()
                # Last sentence complete.
                if not word:
                    sentences.append(sentence)
                    return sentences

                # Sentence complete.
                if word == "\n":
                    sentences.append(sentence)
                    sentence = None
                    continue
                
                # Add normal word.
                clean_word = word.strip()
                sentence.add_word(clean_word)

def read_k_words_from_file(file_name, k):
    ''' Read the first k words found in a file. Each word is seperated by newline char. '''
    with open(file_name) as f:
        data = f.readlines()[:k]
    return [word.strip() for word in data]

if (__name__ == "__main__"):
    # validate_len_args(sys.argv)
    # validate_file_names(sys.argv)
    # train_file = read_sentences_from_file(sys.argv[1])
    # test_file = read_sentences_from_file(sys.argv[2])
    features = read_k_words_from_file(sys.argv[3], sys.argv[4])
    # print ("Argument List:" + str(sys.argv))