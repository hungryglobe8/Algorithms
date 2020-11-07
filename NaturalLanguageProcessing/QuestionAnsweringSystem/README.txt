This is the documentation for my Question Answering system.
IMPORTANT:
The current directory to run qa must be QuestionAnsweringSystem itself.
This will be more flexible later, but I didn't have time to change it before the midpoint eval.
For example: python code\qa.py tests\single.input
More generally: python code\qa.py <input_file>

The system is built into several parts:
qa.py
Split apart an input file into directory location and individual file names.
Combine these to read stories, questions associated with those stories,
and make response files to each of these (in the same directory).

qa_io.py (input/output) (bad name!)
Defines question and answer classes. Questions are seperated by type,
who, when, where, what, why, and how (subtypes on how).
If a more specific algorithm has not been found, attempts to use the most likely sentence.

validate.py
Check file and directory names as well as other args.
reader.py
Read stories, and questions from text files.
Also used for writing response files.

story.py
Story can be used to parse a story file into its important components.
This includes details about the story, included in the header,
and the text, which is divided into several paragraphs.
These can be queried to find the most likely paragraph, sentence, and answer.


Outside resources:
Currently I am using spacy for named entity recognition and part of speech tagging.
I mostly use the named entity recognition for who, where, when, and how (quntities) questions.
I load it in qa.py because I think it helps speed up the overall process.

I would like to use WordNet to find synonyms for important question words, which could be a useful way 
of improving the algorithm I use to find sentences which most likely contain the correct answer.

Installations:
pip install -U spacy (part of speech tagger)
python -m spacy download en_core_web_sm (download english model)