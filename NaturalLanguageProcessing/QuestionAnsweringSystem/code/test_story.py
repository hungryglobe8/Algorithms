import pytest
import story
test_folder = "/NaturalLanguageProcessing/QuestionAnsweringSystem/tests/smalldevset/"

class TestStory():
    def read_test_story_nova_scotia(self):
        test_file = "." + test_folder + "1999-W02-5"

        return story.read_story(test_file)

    def test_read_story_is_not_null(self):
        assert self.read_test_story_nova_scotia() is not None

    def test_read_story_headlines(self):
        sut = self.read_test_story_nova_scotia()

        assert sut.headline == ["Nova", "Scotia", "School", "Pumps", "Up"]
        assert sut.date == ["January", "8,", "1999"]
        assert sut.storyid == "1999-W02-5"

    def test_read_story_paragraphs(self):
        sut = self.read_test_story_nova_scotia()

        assert len(sut.paragraphs) == 13

    def test_get_poss_paragraphs(self):
        sut = self.read_test_story_nova_scotia()
        words = "Where is South Queens Junior High School located?".split()

        res = sut.get_possible_paragraphs(words)

        assert sum(res.values()) == 10

    def test_get_poss_paragraphs_no_stopwords(self):
        sut = self.read_test_story_nova_scotia()
        words = "Where is South Queens Junior High School located?".split()

        res = sut.get_possible_paragraphs(words, story.stopwords)

        assert sum(res.values()) == 7

    def test_use_spacy_get_ents(self):
        sut = self.read_test_story_nova_scotia()

        res = sut.get_named_entities()

        assert res is not None

class TestParagraph():
    def make_paragraph(self, lines=[]):
        ''' Returns a basic paragraph from a list of sentences. '''
        par = story.Paragraph()
        for line in lines:
            par.add_words(line)
        return par

    def test_make_paragraph_is_not_null(self):
        assert self.make_paragraph() is not None

    def test_make_one_paragraph(self):
        text = ["I went\n", "to the store\n", "to fetch water\n", "\n", "\n"]

        res = self.make_paragraph(text)

        assert len(res) == 8