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