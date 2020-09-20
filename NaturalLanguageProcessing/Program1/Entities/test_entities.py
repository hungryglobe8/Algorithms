import pytest
import entities
test_folder = "./NaturalLanguageProcessing/Program1/Entities/"

@pytest.mark.parametrize("word, expected", [
    ("Israel", "yes"),
    ("IBM", "yes"),
    ("nothing", "no")
])
def test_capitalized(word, expected):
    res = entities.tagged_word.is_capitalized(word) 
    
    assert res == expected

def comparison_string(word, pos="n/a", abbr="n/a", cap="n/a"):
    return f"WORD: {word}\nPOS: {pos}\nABBR: {abbr}\n" + \
            f"CAP: {cap}\nWORDCON: n/a\nPOSCON: n/a"

word_cap = ("Israel", "NNP", ["WORD", "CAP"], comparison_string("Israel", cap="yes"))
israel_all = ("Israel", "NNP", ["WORD", "CAP", "POS", "ABBR"], comparison_string("Israel", "NNP", "no", "yes"))
@pytest.mark.parametrize("word, pos, tags, expected", [word_cap, israel_all])
def test_tagged_word(word, pos, tags, expected):
    sut = entities.tagged_word("B-LOC", pos, word)

    res = sut.readable_format(tags)

    assert res == expected

def test_read_words_simple_file():
    test_file = test_folder + "simple_test.txt"

    sut = entities.read_words_from_file(test_file)

    assert len(sut) == 23
    
def test_get_features():
    test_file = test_folder + "simple_test.txt"

    tagged_words = entities.read_words_from_file(test_file)

    total = entities.get_features(tagged_words, ["WORD", "POS", "ABBR", "CAP"])
    assert len(total) == 32

    feature_set = entities.feature_set(total)
    assert len(feature_set) == 32
    assert list(feature_set.values()) == [x for x in range(1, 33)]
        
def test_get_word_feature_vector(order):
    test_file = test_folder + "simple_test.txt"
    words = entities.read_words_from_file(test_file)
    feature_set = simple_feature_set(words, order)

    for word in words:
        print(word.get_feature_vector(order, feature_set))

def test_debug_mode():
    train_file = test_folder + "trainE.txt"
    test_file = test_folder + "testE.txt"
    args = [None, train_file, test_file, "WORD"]

    entities.main(args)


def simple_feature_set(words, order):
    total = entities.get_features(words, order)
    return entities.feature_set(total)
    
if __name__ == "__main__":
    order = ["WORD", "POS", "ABBR", "CAP"]
    test_get_word_feature_vector(order)
    print("switch")
    order = ["WORD", "POSCON", "WORDCON", "POS", "ABBR", "CAP"]
    test_get_word_feature_vector(order)
