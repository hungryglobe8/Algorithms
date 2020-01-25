using Microsoft.VisualStudio.TestTools.UnitTesting;
using MrAnaga;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestAnagrams
{
    [TestClass]
    public class AnagramFinder2Tests
    {
        AnagramFinder2 _sut;
        [TestInitialize]
        public void Setup()
        {
            _sut = new AnagramFinder2();
        }

        [TestMethod]
        public void Initialize()
        {
            Assert.IsNotNull(_sut);
        }

        [TestMethod]
        public void Given_Set_Of_Words_Returns_Unique_Anagrams()
        {
            int numWords = 3;
            int wordLength = 3;
            IEnumerable<string> words = new List<string> { "cat", "dog", "tac" };
            int expected = 1;

            int actual = _sut.FindUniqueAnagrams(numWords, wordLength, words);

            Assert.AreEqual(expected, actual);
        }


    }
}
