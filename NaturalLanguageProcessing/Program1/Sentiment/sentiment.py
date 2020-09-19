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
        if not os.path.isfile(arg):
            raise ValueError(f"{arg} is not a valid file.")

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
            self.features = []

    def add_word(self, word):
        ''' Add a new word to the representation of a sentence. '''
        self.words.append(word)

    def get_feature_vectors(self, data):
        ''' 
        Get the feature vectors for a sentence, according to a feature set.
        Only adds the id of the feature set, if a word is found in it.
        '''
        self.features.clear()
        for word in self.words:
            word_id = get_feature_id(word, data)
            # Append if a word is matched in data, and is not already in self.features.
            if word_id != 0 and word_id not in self.features:
                self.features.append(word_id)
        # Sort features for future use.
        self.features = sorted(self.features)
        return self.features

    def __str__(self):
        data = self.features
        data_str = ""
        for elm in data:
            data_str += f"{elm}:1 "
        return f"{self.label} " + data_str.strip()
        
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

def get_feature_id(word, data):
    ''' Return the feature ID of a given word. '''
    if word in data:
        return data.index(word) + 1
    else:
        return 0

def make_file(old_file_name, sentences):
    new_file_name = old_file_name + ".vector"
    with open(new_file_name, 'w') as f:
        f.writelines([str(sentence) + "\n" for sentence in sentences[:-1]])
        f.write(str(sentences[-1]))

def main(args):
    validate_len_args(args)
    validate_file_names(args)
    train_file = read_sentences_from_file(sys.argv[1])
    test_file = read_sentences_from_file(sys.argv[2])
    k = int(sys.argv[4])
    features = read_k_words_from_file(sys.argv[3], k)

    for sentence in train_file:
        sentence.get_feature_vectors(features)
    for sentence in test_file:
        sentence.get_feature_vectors(features)

    make_file(sys.argv[1], train_file)
    make_file(sys.argv[2], test_file)

if (__name__ == "__main__"):
    main(sys.argv)