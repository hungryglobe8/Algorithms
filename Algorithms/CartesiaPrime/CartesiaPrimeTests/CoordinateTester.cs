using Cartesia_Prime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartesiaPrimeTests
{
    [TestClass]
    public class CoordinateTester
    {
        [TestMethod]
        public void Init()
        {
            Coordinate sut = new Coordinate(0, 0);

            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void Add_2_Expected()
        {
            Coordinate sut = new Coordinate(1, 1);
            Coordinate other = new Coordinate(0, 3);

            ISet<Coordinate> actual = sut + other;
            Assert.IsTrue(actual.Count == 2);

        }

        [TestMethod]
        public void Add_4_Expected()
        {
            Coordinate sut = new Coordinate(1, 1);
            Coordinate other = new Coordinate(1, 3);

            ISet<Coordinate> actual = sut + other;
            Assert.IsTrue(actual.Count == 4);

        }

        [TestMethod]
        public void Add_1_Expected()
        {
            Coordinate sut = new Coordinate(1, 1);
            Coordinate other = new Coordinate(0, 0);

            ISet<Coordinate> actual = sut + other;
            Assert.IsTrue(actual.Count == 1);

        }

        [TestMethod]
        public void HashCode()
        {
            Coordinate c1 = new Coordinate(1, 1);
            Coordinate c2 = new Coordinate(1, 1);

            Assert.AreEqual(c1.GetHashCode(), c2.GetHashCode());
        }
    }
}