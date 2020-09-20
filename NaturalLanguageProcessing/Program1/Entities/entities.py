'''
Contains the code to prepare data for named entity classification. 
This program accepts sentences from movie reviews
as input and labels each sentence as Positive or Negative.
'''
import sys
import os
import re
train_file = ""
test_file = ""
ftypes = ["WORD"]
acceptable_ftypes = ["POSCON", "POS", "ABBR", "WORDCON", "CAP"]
features = None

def validate_len_args(args):
    ''' 
    Format to run sentiment is as follows:
    python <path to sentiment.py> <train file> <test file> <features file> <k>
    There should be 5 arguments total in the command line.
    '''
    if len(args) < 4:
        raise ValueError("There should be at least four arguments following your program.")
    
def validate_file_names(args):
    '''
    Check that files exist, from the current directory.
    
    Don't forget to cd to correct directory!
    '''
    for arg in args[1:2]:
        print(arg)
        if not os.path.isfile(arg):
            raise ValueError(f"{arg} is not a valid file.")

class tagged_word():
    class_labels = {"O": 0, "B-PER": 1, "I-PER": 2, "B-LOC": 3, "I-LOC": 4, "B-ORG": 5, "I-ORG": 6}

    def __init__(self, label, pos, word, features):
        ''' label is the class label. pos is the part-of-speech tag. '''
        self.label = label
        self.pos = pos
        self.word = word
        self.abbr = self.is_abbreviation(word)
        self.cap = self.is_capitalized(word)
        self.wordcon = "n/a"
        self.poscon = "n/a"
        self.features = features
        self.ftypes = {"WORD": self.word, "POS": self.pos, "ABBR": self.abbr, "CAP": self.cap, "WORDCON": self.wordcon, "POSCON": self.poscon}

    @staticmethod
    def from_features(label, pos, word, features, feature_list):
        if pos not in feature_list:
            pos = "UNKPOS"
        if word not in feature_list:
            word = "UNK"
        return tagged_word(label, pos, word, features)

    def readable_format(self, feature_list):
        ''' 
        Print a human readable format of a word identified by its features.
        If a feature is not identified in features, print n/a.
        Follows the order [WORD, POS, ABBR, CAP, WORDCON, POSCON].
        '''
        # Check for unknown word.
        if self.word not in feature_list:
            self.word = "UNK"
        result = f"WORD: {self.word}\n"
        poss = {"POS": self.pos, "ABBR": self.abbr, "CAP": self.cap, "WORDCON": self.wordcon, "POSCON": self.poscon}
        for key, value in poss.items():
            # Check for unkown pos.
            if key == "POS":
                if value not in feature_list:
                    value = "UNKPOS"

            if key in self.features:
                result += f"{key}: {value}\n"
            else:
                result += f"{key}: n/a\n"
        return result

    def get_feature_vector(self, feature_set):
        feature_str = str(self.class_labels[self.label]) + " "
        for item in self.features:
            feature = None
            if item == "ABBR" and self.abbr == "yes":
                feature = "is_abbreviated"
            elif item == "CAP" and self.cap == "yes":
                feature = "is_capitalized"
            elif item == "WORD":
                word = self.ftypes[item]
                if word in feature_set:
                    feature = word
                else:
                    feature = "UNK"
            elif item == "POS":
                pos = self.ftypes[item]
                if pos in feature_set:
                    feature = pos
                else:
                    feature = "UNKPOS"
            else:
                continue
            feature_str += str(feature_set[feature]) + ":1 "
        return feature_str.strip()

    @staticmethod
    def is_abbreviation(word):
        if re.match(r"^[a-zA-Z][a-zA-Z.]{,2}\.$", word):
            return "yes"
        else:
            return "no"

    @staticmethod
    def is_capitalized(word):
        if word[0].isupper():
            return "yes"
        else:
            return "no"

class feature_vector():
    '''
    Class labels are defined by a dictionay.
    self.features is a list of all feature IDs present for a given word.
    '''
    class_labels = {"O": 0, "B-PER": 1, "I-PER": 2, "B-LOC": 3, "I-LOC": 4, "B-ORG": 5, "I-ORG": 6}
    
    def __init__(self, label, features):
        self.label = self.class_labels[label]
        self.features = sorted(features)

    def __str__(self):
        data = self.features
        data_str = ""
        for elm in data:
            data_str += f"{elm}:1 "
        return f"{self.label} {data_str}".strip()

def get_features(tagged_words, ftypes):
    '''
    Generate a feature set based off of several keywords. WORD should be first.
    '''
    # Gather all possible tags.
    unique_words = list()
    unique_pos = list()
    for item in tagged_words:
        if item.word not in unique_words:
            unique_words.append(item.word)
        if item.pos not in unique_pos:
            unique_pos.append(item.pos)
    unique_words.append("UNK")
    unique_pos.append("UNKPOS")
    #ABBR
    abbr = ["is_abbreviated"]
    #CAP
    cap = ["is_capitalized"]
    
    # Append tags in the specified order.
    order = {"WORD" : unique_words, "POSCON" : [], "POS" : unique_pos, "ABBR": abbr, "WORDCON": [], "CAP" : cap}
    features = []
    for ftype in ftypes:
        features += order[ftype]

    return features

def feature_set(features):
    ''' 
    Make unique identifiers for all args.
    Include UNK and UNKPOS as needed.
    '''
    identifiers = {}
    id_number = 1
    for feature in features:
        identifiers[feature] = id_number
        id_number += 1
    return identifiers    

def read_words_from_file(file_name, features):
    ''' Read words in a file. Each word has attributes seperated by tabs and sentences are seperated by newline char. '''
    words = []
    with open(file_name) as f: 
        while(True):
            word = f.readline()
            # Last sentence complete.
            if not word:
                return words

            word = word.split()
            # Sentence complete.
            if word == []:
                continue
            
            # Add normal word.
            prepared_word = tagged_word(word[0], word[1], word[2], features)
            words.append(prepared_word)

def make_readable_files(feature_set, **kwargs):
    for old_file_name, words in kwargs.items():
        new_file_name = old_file_name + ".readable"
        with open(new_file_name, 'w') as f:
            f.writelines([word.readable_format(feature_set) + "\n" for word in words[:-1]])
            f.write(words[-1].readable_format(feature_set).strip())

def make_feature_files(feature_set, **kwargs):
    for old_file_name, words in kwargs.items():
        new_file_name = old_file_name + ".vector"
        with open(new_file_name, 'w') as f:
            f.writelines([word.get_feature_vector(feature_set) + "\n" for word in words[:-1]])
            f.write(words[-1].get_feature_vector(feature_set))

def parse_args(args):
    ''' Check validity and return args in necessary formats. '''
    validate_len_args(args)
    validate_file_names(args)
    return args[1], args[2], args[3:]

def main(args):
    train_file, test_file, features = parse_args(args)
    # Save word lists.
    train_words = read_words_from_file(train_file, features)
    test_words = read_words_from_file(test_file, features)
    # Link files and their respective words.
    kwargs = {train_file: train_words, test_file: test_words}
    # Get feature set.
    partial_features = get_features(train_words, features)
    full_feature_set_with_ids = feature_set(partial_features)
    # Make readable files.
    make_readable_files(full_feature_set_with_ids, **kwargs)
    # Make feature vector files.
    make_feature_files(full_feature_set_with_ids, **kwargs)

if (__name__ == "__main__"):
    main(sys.argv)