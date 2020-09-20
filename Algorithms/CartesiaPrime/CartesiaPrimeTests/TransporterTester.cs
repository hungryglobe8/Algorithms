using Cartesia_Prime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CartesiaPrimeTests
{
    [TestClass]
    public class TransporterTester
    {
        [TestMethod]
        public void Init()
        {
            Transporter sut = new Transporter(0, 0, 0, 0);

            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void GivenTest()
        {
            Transporter sut = new Transporter(2, 3, 1, 4);
            Coordinate expected = new Coordinate(2, 1);

            Coordinate actual = sut.Move(1);
            Assert.AreEqual(expected, actual);

            expected = new Coordinate(1, 2);
            actual = sut.Move(2);
            Assert.AreEqual(expected, actual);

            expected = new Coordinate(0, 3);
            actual = sut.Move(3);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Ones_For_b_And_d()
        {
            Transporter sut = new Transporter(10, 1, 5, 1);
            Coordinate expected = new Coordinate(0, 0);

            Coordinate actual = sut.Move(15);

            Assert.AreEqual(expected, actual);
        }
    }
}
