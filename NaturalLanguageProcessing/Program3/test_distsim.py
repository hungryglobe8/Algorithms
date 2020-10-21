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

def test_get_context_window_with_k():
    test_file = test_folder + "test.txt"
    stopwords_file = test_folder + "stopwords.txt"
    sentences = distsim.read_from_file(test_file, distsim.Sentence)
    stopwords = distsim.read_from_file(stopwords_file)
    
    res = distsim.get_context_window(sentences, 0, stopwords)

    assert len(res) == 11

def test_get_train_vocab_with_k():
    test_file = test_folder + "train.txt"
    stopwords_file = test_folder + "stopwords.txt"
    sentences = distsim.read_from_file(test_file, distsim.GoldSentence)
    stopwords = distsim.read_from_file(stopwords_file)
    
    res = distsim.get_context_window(sentences, 2, stopwords)

    assert len(res) == 595

def test_get_unique_senses():
    test_file = test_folder + "train.txt"
    sentences = distsim.read_from_file(test_file, distsim.GoldSentence)
    
    res = distsim.get_unique_senses(sentences)

    assert len(res) == 6

def test_create_training_sig_vectors():
    test_file = test_folder + "train.txt"
    stopwords_file = test_folder + "stopwords.txt"
    sentences = distsim.read_from_file(test_file, distsim.GoldSentence)
    stopwords = distsim.read_from_file(stopwords_file)
    k = 2
    
    vocab = distsim.get_context_window(sentences, k, stopwords)
    senses = distsim.get_unique_senses(sentences)

    signature_vectors = distsim.make_sig_vectors(senses, sentences, vocab, k)

    # for vec in signature_vectors.values():
    #     print(str(vec))

    assert len(signature_vectors) == 6

def test_create_test_sig_vectors():
    train_file = test_folder + "train.txt"
    test_file = test_folder + "test.txt"
    stopwords_file = test_folder + "stopwords.txt"
    sentences = distsim.read_from_file(train_file, distsim.GoldSentence)
    test_sentences = distsim.read_from_file(test_file, distsim.Sentence)
    stopwords = distsim.read_from_file(stopwords_file)
    k = 2
    
    vocab = distsim.get_context_window(sentences, k, stopwords)
    senses = distsim.get_unique_senses(sentences)

    train_signature_vectors = distsim.make_sig_vectors(senses, sentences, vocab, k)
    test_signature_vectors = distsim.make_norm_sig_vectors(senses, test_sentences, vocab, k)

    for vec in test_signature_vectors.values():
        print(str(vec))

    assert len(test_signature_vectors) == 6

def test_compute_cosine_similarity():
    train_file = test_folder + "train.txt"
    test_file = test_folder + "test.txt"
    stopwords_file = test_folder + "stopwords.txt"
    sentences = distsim.read_from_file(train_file, distsim.GoldSentence)
    test_sentences = distsim.read_from_file(test_file, distsim.Sentence)
    stopwords = distsim.read_from_file(stopwords_file)
    k = 10
    
    vocab = distsim.get_context_window(sentences, k, stopwords)
    senses = distsim.get_unique_senses(sentences)

    train_signature_vectors = distsim.make_sig_vectors(senses, sentences, vocab, k)
    test_signature_vectors = distsim.make_norm_sig_vectors(senses, test_sentences, vocab, k)

    test_scores = list()
    for vec in test_signature_vectors.values():
        sentence_scores = list()
        for other_vec in train_signature_vectors.values():
            sentence_scores.append(vec.cosine_sim(other_vec, other_vec.sense))
        test_scores.append(sentence_scores)

    res = distsim.sorted_scores(test_scores)
    print(res)
    assert len(test_signature_vectors) == 6

test_compute_cosine_similarity()