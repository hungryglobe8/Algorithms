using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnderTheRainbow;
using System;
using System.IO;

namespace UnitTests
{
    [TestClass]

    public class InputTester
    {
        [TestMethod]
        public void BasicInput()
        {
            var input = new StringReader("3\n0\n350\n450\n825");
            Console.SetIn(input);
            InputReader sut = new InputReader();

            int[] actual = sut.Distances;

            Assert.AreEqual(4, actual.Length);
        }

        [TestMethod]
        public void BasicInput2()
        {
            var input = new StringReader("1\n0\n450");
            Console.SetIn(input);
            InputReader sut = new InputReader();

            int[] actual = sut.Distances;

            Assert.AreEqual(2, actual.Length);
        }
    }
}
