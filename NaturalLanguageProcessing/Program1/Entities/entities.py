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
    ''' label is the class label. pos is the part-of-speech tag. '''
    def __init__(self, label, pos, word):
        self.label = label
        self.pos = pos
        self.word = word
        self.abbr = self.is_abbreviation(word)
        self.cap = self.is_capitalized(word)
        self.wordcon = "n/a"
        self.poscon = "n/a"

    def readable_format(self, features):
        ''' 
        Print a human readable format of a word identified by its features.
        If a feature is not identified in features, print n/a.
        Follows the order [WORD, POS, ABBR, CAP, WORDCON, POSCON].
        '''
        result = f"WORD: {self.word}\n"
        poss = {"POS": self.pos, "ABBR": self.abbr, "CAP": self.cap, "WORDCON": self.wordcon, "POSCON": self.poscon}
        for key, value in poss.items():
            if key in features:
                result += f"{key}: {value}\n"
            else:
                result += f"{key}: n/a\n"
        return result.strip()

    @staticmethod
    def is_abbreviation(word):
        if len(word) > 4:
            return "no"
        elif re.match(r"[a-zA-Z.]{,3}\.$", word):
            return "yes"
        else:
            return "no"

    @staticmethod
    def is_capitalized(word):
        if word[0].isupper():
            return "yes"
        else:
            return "no"

def main(args):
    pass

if (__name__ == "__main__"):
    main(sys.argv)