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

    assert res.sense == "phone"
    assert len(res.definition) == 3
    assert len(res.example) == 4

test_single_sense_stored_correctly()