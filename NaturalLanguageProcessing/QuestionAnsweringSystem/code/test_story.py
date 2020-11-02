import pytest
import story
test_folder = "./NaturalLanguageProcessing/QuestionAnsweringSystem/tests/"

def test_read_story_headlines():
    test_file = test_folder + "1999-W02-5.story"

    assert story.read_story(test_file) is not None

test_read_story_headlines()