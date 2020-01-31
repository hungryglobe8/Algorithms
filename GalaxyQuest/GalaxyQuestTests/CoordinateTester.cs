using GalaxyQuest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GalaxyQuestTests
{
    [TestClass]
    public class CoordinateTester
    {
        [TestMethod]
        public void Initialize()
        {
            Coordinate sut = new Coordinate(10, 10);

            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void ObjectsNotSame()
        {
            Coordinate c1 = new Coordinate(1, 5);
            Coordinate c2 = new Coordinate(1, 5);

            Assert.AreNotEqual(c1, c2);
        }

        [TestMethod]
        public void XIsDiff_IsWithinRange()
        {
            Coordinate c1 = new Coordinate(21, 5);
            Coordinate c2 = new Coordinate(1, 5);

            Assert.IsTrue(c1.IsWithinRange(c2, 21));
            Assert.IsTrue(c2.IsWithinRange(c1, 20));
        }

        [TestMethod]
        public void YIsDiff_IsWithinRange()
        {
            Coordinate c1 = new Coordinate(5, 35);
            Coordinate c2 = new Coordinate(5, 5);

            Assert.IsTrue(c1.IsWithinRange(c2, 30));
            Assert.IsTrue(c2.IsWithinRange(c1, 30));
        }

        [TestMethod]
        public void OutsideOfRange()
        {
            Coordinate c1 = new Coordinate(10, 10);
            Coordinate c2 = new Coordinate(6, 6);

            Assert.IsFalse(c1.IsWithinRange(c2, 5));
        }

        [TestMethod]
        public void LargeCoordinates()
        {
            Coordinate c1 = new Coordinate(900000000, 800000000);
            Coordinate c2 = new Coordinate(700000000, 600000000);

            long distance = (long)3e8;
            long shortDistance = (long)3e7;

            Assert.IsTrue(c1.IsWithinRange(c2, distance));
            Assert.IsFalse(c1.IsWithinRange(c2, shortDistance));
        }
    }
}
