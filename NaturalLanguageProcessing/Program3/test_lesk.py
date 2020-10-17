import pytest
import lesk
test_folder = "./NaturalLanguageProcessing/Program3/data/"

def test_read_sentences():
    test_file = test_folder + "test.txt"

    res = lesk.read_from_file(test_file, lesk.Sentence)

    assert len(res) == 6

def test_read_definitions():
    test_file = test_folder + "definitions.txt"

    senses = lesk.read_from_file(test_file, lesk.Sense)

    assert len(senses) == 6

def test_single_sense_stored_correctly():
    test_file = test_folder + "definitions.txt"
    senses = lesk.read_from_file(test_file, lesk.Sense)

    res = senses[0]

    assert res.root == "phone"
    assert len(res.definition) == 3
    assert len(res.example) == 4

def test_read_stopwords():
    test_file = test_folder + "stopwords.txt"

    stopwords = lesk.read_from_file(test_file)

    assert len(stopwords) == 179

def test_get_context():
    test_file = test_folder + "stopwords.txt"
    test_sentence = lesk.Sentence("I went to the store a day . what to do? store went")
    stopwords = lesk.read_from_file(test_file)

    res = lesk.get_context(stopwords, test_sentence.words)

    assert len(res) == 4

def test_print_lesk():
    lesk_dict = {
        4: ["banana", "grape", "apple", "donut"],
        1: ["mess"],
        3: ["tooth", "cat"]
        }

    res = lesk.get_ordered_lesk(lesk_dict)

    assert res == 'apple(4) banana(4) donut(4) grape(4) cat(3) tooth(3) mess(1)'