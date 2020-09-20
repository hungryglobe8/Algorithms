using Cartesia_Prime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartesiaPrimeTests
{
    [TestClass]
    public class GridTester
    {
        [TestMethod]
        public void Init()
        {
            Grid sut = new Grid();

            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void SampleInput1()
        {
            Grid sut = new Grid();
            Transporter trans = new Transporter(2, 3, 1, 4);

            int result = sut.ShortestPath(trans, 3, new Coordinate(3, 2));

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void SampleInput2()
        {
            Grid sut = new Grid();
            Transporter trans = new Transporter(2, 3, 1, 4);

            int result = sut.ShortestPath(trans, 2, new Coordinate(3, 2));

            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void SampleInput3()
        {
            Grid sut = new Grid();
            Transporter trans = new Transporter(1, 10, 1, 10);
            sut.AddDrone(-3, 3);

            int result = sut.ShortestPath(trans, 5, new Coordinate(-4, 4));

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void SampleInput4()
        {
            Grid sut = new Grid();
            Transporter trans = new Transporter(1, 2, 1, 2);
            sut.AddDrone(-1, -1);

            int result = sut.ShortestPath(trans, 3, new Coordinate(-2, -2));

            Assert.AreEqual(-1, result);
        }
    }
}
