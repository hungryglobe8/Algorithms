import pytest
import distsim
test_folder = "./NaturalLanguageProcessing/Program3/data/"

def test_read_sentences():
    test_file = test_folder + "test.txt"

    res = distsim.read_from_file(test_file, distsim.Sentence)

    assert len(res) == 2487

def test_read_training_senses():
    test_file = test_folder + "train.txt"

    senses = distsim.read_from_file(test_file, distsim.GoldSentence)

    print(len(senses))

    assert len(senses) == 6

test_read_training_senses()