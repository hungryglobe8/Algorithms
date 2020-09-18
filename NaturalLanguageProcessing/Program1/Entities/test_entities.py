import pytest
import entities

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

if __name__ == "__main__":
    test_tagged_word("Israel", "NNP", ["WORD", "CAP"], comparison_string("Israel", cap="yes"))