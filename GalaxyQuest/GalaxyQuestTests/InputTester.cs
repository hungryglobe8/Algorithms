using GalaxyQuest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace GalaxyQuestTests
{
    [TestClass]
    public class InputTester
    {
        private InputReader sut;
        [TestMethod]
        public void Initialize()
        {
            sut = new InputReader();

            Assert.IsNotNull(sut);
        }

        [TestInitialize]
        public void SetUp()
        {
            sut = new InputReader();
        }

        [TestMethod]
        public void Diameter()
        {
            var input = new StringReader("10 4 \n45 46 \n90 47 \n45 54 \n90 43");
            Console.SetIn(input);
            sut.ReadInput();

            int actual = sut.GetDiameter();

            Assert.AreEqual(10, actual);
        }

        [TestMethod]
        public void NumStars_Short()
        {
            var input = new StringReader("10 4 \n45 46 \n90 47 \n45 54 \n90 43");
            Console.SetIn(input);
            sut.ReadInput();

            var actual = sut.GetStars();

            Assert.AreEqual(4, actual.Count);
        }

        [TestMethod]
        public void NumStars_Medium()
        {
            var input = new StringReader("20 7\n1 1\n100 100\n1 3\n101 101\n3 1\n102 102\n3 3");
            Console.SetIn(input);
            sut.ReadInput();

            var actual = sut.GetStars();

            Assert.AreEqual(7, actual.Count);
        }

        //TODO: LONG Generate string?
    }
}
