import pytest
import distsim
test_folder = "./NaturalLanguageProcessing/Program3/data/"

def test_read_sentences():
    test_file = test_folder + "test.txt"

    res = distsim.read_from_file(test_file, distsim.Sentence)

    assert len(res) == 6

def test_read_training_senses():
    test_file = test_folder + "train.txt"

    senses = distsim.read_from_file(test_file, distsim.GoldSentence)

    print({x.sense for x in senses})
    assert len(senses) == 2487

def test_get_vocab_with_k():
    test_file = test_folder + "test.txt"
    stopwords_file = test_folder + "stopwords.txt"

    sentences = distsim.read_from_file(test_file, distsim.Sentence)
    stopwords = distsim.read_from_file(stopwords_file)
    
    for sentence in sentences:
        sentence.get_vocab(0, stopwords)

test_get_vocab_with_k()