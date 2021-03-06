'''
Contains an implementation of a simplified Lesk algorithm
to determine the best sense for each instance of a target word.

Can be run as:
python3 lesk.py test.txt definitions.txt stopwords.txt
'''
import sys, os, re, math

def parse_args(args):
    ''' Check validity and return args in necessary formats. '''
    validate_len_args(args)
    validate_file_names(args)
    return args[1], args[2], args[3]

def validate_len_args(args):
    ''' 
    There should be 3 arguments following lesk.py in the command line.
    '''
    if len(args) != 4:
        raise ValueError("There should be three arguments following your program.")
    
def validate_file_names(args):
    '''
    Check that files exist, from the current directory.
    
    Don't forget to cd to correct directory!
    '''
    for arg in args[1:4]:
        if not os.path.isfile(arg):
            raise ValueError(f"{arg} is not a valid file.")

def read_from_file(file_name, class_name=None):
    ''' 
    Read something from a file. Scans by new lines and class_name specifies how to parse the line.
    If class_name is not specified, return a straight list the file seperated by newlines.
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
                # Might not work
                if class_name is not None:
                    sentences.append(class_name(line))
                else:
                    sentences.append(line.strip())

class Sentence():
    ''' Contains a list of words surrounding a target word. '''
    def __init__(self, line):
        self.words = line.split()
        lead = "<occurrence>"
        tail = "</>"
        for word in self.words:
            if word.startswith(lead) and word.endswith(tail):
                self.root = word.lstrip(lead).rstrip(tail)
                return

    def __len__(self):
        return len(self.words)

    def __str__(self):
        res = ""
        for word in self.words:
            res += f"{word} "
        return res.strip()

class Sense():
    ''' Contains a sense, the definition, and an example sentence. '''
    def __init__(self, line):
        sections = line.split('\t')
        self.root = sections[0]
        self.definition = sections[1].split()
        self.example = sections[2].split()

    def __str__(self):
        return f"{self.root}: {' '.join(self.definition)}\n\t{' '.join(self.example)}"

    # def __lt__(self, other):
    #     return self.root < other.root

def get_context(stopwords, words):
    ''' Returns a list of valid words from a list of words (not in stopwords or punctuation). '''
    res = set()
    for word in words:
        word = word.lower()
        if word not in stopwords and re.match(r"[a-z]", word):
            res.add(word)
    return res

def compute_overlap(signature, context):
    ''' Returns the number of elements in common between two sets. '''
    return len(signature.intersection(context))

def run_lesk(senses, stopwords, sentence):
    ''' Returns a string as a ranked list of overlaps of sense definitions with a sentence. '''
    lesk_dict = {}
    context = get_context(stopwords, sentence)
    for sense in senses:
        signature = get_context(stopwords, sense.definition).union(get_context(stopwords, sense.example))
        overlap = compute_overlap(signature, context)
        if overlap in lesk_dict.keys():
            lesk_dict[overlap].append(sense.root)
        else:
            lesk_dict[overlap] = [sense.root]
    
    return get_ordered_lesk(lesk_dict)

def get_ordered_lesk(dictionary):
    ''' Return a sorted string of a dictionary set up with integers as the keys and strings as values. '''
    res = ""
    # Continue the loop until there is nothing left in the dictionary.
    while len(dictionary) != 0:
        max_value = max(dictionary.keys())
        sorted_strings = sorted(dictionary[max_value])
        for string in sorted_strings:
            res += f"{string}({max_value}) "
        
        dictionary.pop(max_value)
    # Remove trailing whitespace.
    return res.strip()

def make_file(old_file_name, text):
    new_file_name = old_file_name + ".lesk"
    with open(new_file_name, 'w') as f:
        f.writelines('\n'.join(text))

def main(args):
    test_file, definitions_file, stopwords_file = parse_args(args)
    # Read from files.
    sentences = read_from_file(test_file, Sentence)
    senses = read_from_file(definitions_file, Sense)
    stopwords = read_from_file(stopwords_file)
    # Run lesk algorithm on each sentence.
    text = []
    for sentence in sentences:
        ordered_senses = run_lesk(senses, stopwords, sentence.words)
        text.append(ordered_senses)
    # Print results to new .lesk file.
    make_file(test_file, text)

if (__name__ == "__main__"):
    main(sys.argv)