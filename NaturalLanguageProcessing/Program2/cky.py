'''
Contains the code to prepare data for named entity classification. 
This program accepts sentences from movie reviews
as input and labels each sentence as Positive or Negative.
'''
import sys
import os
import re
pcfg_file = ""
sentences_file = ""

def parse_args(args):
    ''' Check validity and return args in necessary formats. '''
    validate_len_args(args)
    validate_file_names(args)
    return args[1], args[2]

def validate_len_args(args):
    ''' 
    Format to run cky is as follows:
    python <path to cky.py> <pcfg file> <sentences file>
    There should be 3 arguments total in the command line.
    '''
    if len(args) < 3:
        raise ValueError("There should be two arguments following your program.")
    
def validate_file_names(args):
    '''
    Check that files exist, from the current directory.
    
    Don't forget to cd to correct directory!
    '''
    for arg in args[1:2]:
        print(arg)
        if not os.path.isfile(arg):
            raise ValueError(f"{arg} is not a valid file.")

def read_grammar_from_file(file_name):
    ''' Read words in a file. Each word has attributes seperated by tabs and sentences are seperated by newline char. '''
    grammar = Grammar()
    with open(file_name) as f: 
        while(True):
            line = f.readline()
            # Last sentence complete.
            if not line:
                return grammar

            if line != []:
                grammar.add_rule(Rule(line))

def read_sentences_from_file(file_name):
    ''' Read sentences in a file. Each sentence is seperated by newline char and has words seperated by whitespace. '''
    sentences = []
    with open(file_name) as f: 
        while(True):
            line = f.readline()
            # Last sentence complete.
            if not line:
                return sentences

            # Sentence has words.
            if line != []:
                sentences.append(Sentence(line))

class Sentence():
    def __init__(self, line):
        self.words = line.split()

class Rule():
    def __init__(self, line):
        tokens = line.split()
        self.word = tokens[0]
        if len(tokens) == 4:
            self.rule = tokens[2:3]
        else:
            self.rule = tokens[2:4]
        self.prob = tokens[-1]

    def __str__(self):
        return f"{self.word}: {self.rule}, {self.prob}"

    def get_rule(self):
        return self.rule

class Grammar():
    def __init__(self):
        self.rules = list()

    def __len__(self):
        return len(self.rules)

    def add_rule(self, rule):
        self.rules.append(rule)

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


def main(args):
    pcfg_file, sentences_file = parse_args(args)
    # Save word lists.
    grammar_rules = read_rules_from_file(pcfg_file)
    sentences = read_sentences_from_file(sentences_file)
    # Link files and their respective words.
    kwargs = {pcfg_file: grammar_rules, sentences_file: sentences}
    print(kwargs)
    # Get feature set.
    # partial_features = get_features(train_words, features)
    # full_feature_set_with_ids = feature_set(partial_features)
    # Make readable files.
    # make_readable_files(full_feature_set_with_ids, **kwargs)
    # # Make feature vector files.
    # make_feature_files(full_feature_set_with_ids, **kwargs)

if (__name__ == "__main__"):
    main(sys.argv)