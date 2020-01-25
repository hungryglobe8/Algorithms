using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MrAnaga;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestAnagrams
{
    [TestClass]
    public class RandomStringGeneratorTests
    {
        RandomStringGenerator _sut;
        Mock<IRandomNumberGenerator> _mockNumberGenerator;

        [TestInitialize]
        public void Initialize()
        {
            _mockNumberGenerator = new Mock<IRandomNumberGenerator>();
            _sut = new RandomStringGenerator(_mockNumberGenerator.Object);
        }

        [TestMethod]
        public void Instantiate()
        {
            Assert.IsNotNull(_sut);
        }

        [DataTestMethod]
        [DataRow(0, 3, "aaa")]
        [DataRow(0, 5, "aaaaa")]
        [DataRow(25, 4, "zzzz")]
        public void Given_Number_Returns_String_of_Letters(int index, int wordLength, string expected)
        {
            _mockNumberGenerator.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(index);
            var actual = _sut.CreateRandomString(wordLength);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Real_RandomStringGenerator_Returns_Random_String_Of_Correct_Length()
        {
            var sut = new RandomStringGenerator(new RandomNumberGenerator());
            var expected = 5;

            var actual = sut.CreateRandomString(expected);

            Assert.AreEqual(expected, actual.Length);
        }
    }
}
