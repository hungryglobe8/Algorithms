import pytest
import sentiment
test_folder = "./NaturalLanguageProcessing/"
def test_validate():
    args = ["program", "train", "test", "features", "k"]
    
    sentiment.validate_len_args(args)

@pytest.mark.parametrize("test_args", [[1, 2, 3, 4], [1, 2, 3, 4, 5, 6]])
def test_validate_incorrect_length_throws(test_args):
    with pytest.raises(ValueError):
        sentiment.validate_len_args(test_args)

def test_validate_file_names():
    # shouldn't throw valid test files
    args = ["program", test_folder + "test1.txt", test_folder + "test2.txt", 100]
    
    sentiment.validate_file_names(args)

def test_sentences_from_file():
    test_file = test_folder + "test1.txt"

    res = sentiment.read_sentences_from_file(test_file)

    assert len(res) == 4

def test_read_k_words_from_file():
    test_file = test_folder + "test2.txt"

    data = sentiment.read_k_words_from_file(test_file, 10)

    assert len(data) == 10

def test_read_many_words_from_file():
    test_file = test_folder + "test2.txt"

    data = sentiment.read_k_words_from_file(test_file, 20)

    assert len(data) == 16

def test_look_up_feature_id():
    test_file = test_folder + "test2.txt"

    data = sentiment.read_k_words_from_file(test_file, 10)

    assert sentiment.get_feature_id("apple", data) == 5

def test_look_up_invalid_feature_id():
    test_file = test_folder + "test2.txt"

    data = sentiment.read_k_words_from_file(test_file, 10)

    assert sentiment.get_feature_id("quirk", data) == 0

def positive_sentence():
    sentence = sentiment.Sentence(1)
    sentence.add_word("this")
    sentence.add_word("movie")
    sentence.add_word("is")
    sentence.add_word("good")
    sentence.add_word("!")
    return sentence

def negative_sentence():
    sentence = sentiment.Sentence(0)
    sentence.add_word("this")
    sentence.add_word("bad")
    sentence.add_word(",")
    sentence.add_word("bad")
    sentence.add_word("movie")
    sentence.add_word("is")
    sentence.add_word("incredibly")
    sentence.add_word("bad")
    return sentence

def basic_feature_vectors():
    return ["movie", "good", "bad", "!"]

def test_negative_sentence_feature_vectors():
    sut = negative_sentence()

    res = sut.get_feature_vectors(basic_feature_vectors())
    assert res == [1, 3]

    assert str(sut) == "0 1:1 3:1"

def test_positive_sentence_feature_vectors():
    sut = positive_sentence()

    res = sut.get_feature_vectors(basic_feature_vectors())
    assert res == [1, 2, 4]

    assert str(sut) == "1 1:1 2:1 4:1"

def test_neutral_sentence():
    sut = sentiment.Sentence(1)
    sut.add_word("so")
    sut.add_word("exciting")

    res = sut.get_feature_vectors(basic_feature_vectors())
    assert res == []

    assert str(sut) == "1"

def test_all_connected():
    sentence_file = test_folder + "test_sentences.txt"
    feature_file = test_folder + "test_features.txt"
    sentences = sentiment.read_sentences_from_file(sentence_file)
    features = sentiment.read_k_words_from_file(feature_file, 4)
    
    for sentence in sentences:
        sentence.get_feature_vectors(features)
    
    sentiment.make_file(sentence_file, sentences)
    assert 1 == 1

    

