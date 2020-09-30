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

    assert len(two_rules.right_side) == 2
    assert len(one_rule.right_side) == 1

def test_read_grammar_from_file():
    test_file = test_folder + "pcfg-tiny.txt"

    grammar = cky.read_grammar_from_file(test_file)

    assert len(grammar) == 9

def test_get_rules_leading_to_word():
    test_file = test_folder + "pcfg-tiny.txt"
    grammar = cky.read_grammar_from_file(test_file)

    non_terminals = grammar.get_rules_leading_to_word(["fish"])

    assert len(non_terminals) == 2

def test_cfk():
    test_file = test_folder + "pcfg-tiny.txt"
    grammar = cky.read_grammar_from_file(test_file)
    sentence = cky.Sentence("I trust shrinks")

    table, num_parses = grammar.run_cky(sentence)

    return cky.print_cky(sentence, num_parses, table)

def test_example():
    test_file = test_folder + "pcfg-example.txt"
    grammar = cky.read_grammar_from_file(test_file)
    sentence = cky.Sentence("book the flight to Houston to Houston")

    table, num_parses = grammar.run_cky(sentence)

    return cky.print_cky(sentence, num_parses, table)

test_example()