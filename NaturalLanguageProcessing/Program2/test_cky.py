import pytest
import cky
test_folder = "./NaturalLanguageProcessing/Program2/data/"

def test_two_non_terminal_rule():
    line = "S -> NP VP .80"
    
    res = cky.Rule(line)
    
    assert str(res) == "S: ['NP', 'VP'], .80"

def test_terminal_rule():
    line = "NP -> trust .172"

    res = cky.Rule(line)
    
    assert str(res) == "NP: ['trust'], .172"

def test_rule_length():
    two_rules = cky.Rule("S -> NP VP .80")
    one_rule = cky.Rule("NP -> trust .172")

    assert len(two_rules.get_rule()) == 2
    assert len(one_rule.get_rule()) == 1

def test_read_grammar_from_file():
    test_file = test_folder + "pcfg-tiny.txt"

    grammar = cky.read_grammar_from_file(test_file)
    
    assert len(grammar) == 9