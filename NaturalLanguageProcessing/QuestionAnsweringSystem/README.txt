This is the documentation for my Question Answering system.
The system is built into several parts:

story.py
Story can be used to parse a story file into its important components.
This includes details about the story, included in the header,
and the text, which is divided into several paragraphs.
These can be queried to find the most likely paragraph, sentence, and answer.



Installations:
pip install -U spacy (part of speech tagger)
python -m spacy download en_core_web_sm (download english model)