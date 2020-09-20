using Microsoft.VisualStudio.TestTools.UnitTesting;
using MrAnaga;
using System;

namespace TestAnagrams
{
    [TestClass]
    public class UnitTest1
    {
        UniqueAnagramFinder sut;

        [TestInitialize]
        public void TestInitialize()
        {
            sut = new UniqueAnagramFinder("4 2");
        }
        [DataTestMethod]
        [DataRow("4 2", 4, 2)]
        [DataRow("5372 389", 5372, 389)]
        public void ReadShortInputStream(string input, int expectedWordCount, int expectedWordLength)
        {
            var sut = new UniqueAnagramFinder(input);
            sut.ConvertInputTokens();
            Assert.IsTrue(sut.WordCount == expectedWordCount);
            Assert.IsTrue(sut.WordLength == expectedWordLength);
        }

        [TestMethod]
        public void SortShortString()
        {
            string testString = "brick";
            string sortedString = sut.SortString(testString);
            Assert.AreEqual("bcikr", sortedString);
        }

        [TestMethod]
        public void SortAnagrams()
        {
            string testString = "attack";
            string testString2 = "tackta";
            string sortedString = sut.SortString(testString);
            string sortedString2 = sut.SortString(testString2);
            Assert.IsTrue(sortedString == sortedString2);
        }

        [TestMethod]
        public void ConstantKTiming()
        {
            int n = 10;
            double oldResult = 1;
            while (n < 12000)
            {
                double result = sut.ConstantKTiming(n);
                double ratio = result / oldResult;
                Console.WriteLine(n + "\t" + result + "\t" + ratio);
                n *= 2;
                oldResult = result;
            }
        }

        [TestMethod]
        public void ConstantNTiming()
        {
            int k = 5;
            double oldResult = 1;
            while (k < 1000)
            {
                double result = sut.ConstantNTiming(k);
                double ratio = result / oldResult;
                Console.WriteLine(k + "\t" + result + "\t" + ratio);
                k *= 2;
                oldResult = result;
            }
        }
    }
}