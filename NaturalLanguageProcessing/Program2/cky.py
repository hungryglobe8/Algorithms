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

    def __len__(self):
        return len(self.words)

    def __str__(self):
        res = ""
        for word in self.words:
            res += f"{word} "
        return res.strip()

class Rule():
    def __init__(self, line):
        tokens = line.split()
        self.left_side = tokens[0]
        if len(tokens) == 4:
            self.terminals = tokens[2:3]
        else:
            self.terminals = tokens[2:4]
        self.prob = tokens[-1]

    def __str__(self):
        return f"{self.left_side}: {self.terminals}, {self.prob}"

    def get_terminals(self):
        return self.terminals

class Grammar():
    def __init__(self):
        self.rules = list()

    def __len__(self):
        return len(self.rules)

    def add_rule(self, rule):
        self.rules.append(rule)

    def get_rules_leading_to_word(self, right_side):
        rules = set()
        # Go through all rules.
        for rule in self.rules:
            # Check for word in terminals.
            if right_side == rule.terminals:
                rules.add(rule.left_side)

        return rules
    
    def get_rules_leading_to_non_terminals(self, b_words, c_words):
        '''
        Get all rules of the form A -> BC for all possible combinations from
            b_words and c_words.
        '''
        rules = list()
        count = 0
        # Go through all possible combinations of b_words and c_words, adding unique ones.
        for b in b_words:
            for c in c_words:
                poss = self.get_rules_leading_to_word([b, c])
                if len(poss) == 0:
                    pass
                else:
                    count += 1
                    for elm in poss:
                        rules.append(elm)

        return rules, count

def run_cfk(grammar, sentence):
    length = len(sentence)
    table = [[list() for col in range(length + 1)] for row in range(length + 1)]
    num_parses = 0
    # Iterate over columns.
    for c in range(1, length + 1):
        # Add non-terminals for the current word.
        table[c][c] = grammar.get_rules_leading_to_word(sentence.words[c - 1:c])
        # Iterate over rows, from bottom to top.
        for r in range(c-1, 0, -1):
            # Explore all partionings for words 1 through c.
            for s in range(r+1, c+1):
                b_words = table[r][s-1]
                c_words = table[s][c]
                if b_words is None or c_words is None:
                    pass
                else:
                    new_rules, count = grammar.get_rules_leading_to_non_terminals(b_words, c_words)
                    if c == length and r == 1:
                        num_parses += count
                    table[r][c] = table[r][c] + new_rules

    return table, num_parses

def print_cfk(sentence, num_parses, table):
    # Heading information.
    print(f"PARSING SENTENCE: {str(sentence)}")
    print(f"NUMBER OF PARSES FOUND: {num_parses}")
    print("TABLE:")
    # Table.
    length = len(table[0])
    for r in range(1, length):
        for c in range(r, length):
            entries = table[r][c]
            str_result = ""
            if len(entries) == 0:
                str_result = "-"
            else:
                for entry in sorted(entries):
                    str_result += f"{entry} "
                str_result = str_result.strip()
            print(f"cell[{r},{c}]: {str_result}")
    # New line.
    print()
    
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
    # Read from files.
    sentences = read_sentences_from_file(sentences_file)
    grammar = read_grammar_from_file(pcfg_file)
    # Run cky on each sentence.
    for sentence in sentences:
        table, num_parses = run_cfk(grammar, sentence)
        print_cfk(sentence, num_parses, table)
    # print again?

if (__name__ == "__main__"):
    main(sys.argv)