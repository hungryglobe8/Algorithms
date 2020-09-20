using GetShort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GetShortTests
{
    [TestClass]
    public class CorridorTester
    {
        [TestMethod]
        public void Init()
        {
            Corridor sut = new Corridor("0 1 1");

            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void TwoConstructors()
        {
            Corridor c1 = new Corridor("0 1 1");
            Corridor c2 = new Corridor(0, 1, 1);

            Assert.AreEqual(c1.Start, c2.Start);
            Assert.AreEqual(c1.End, c2.End);
            Assert.AreEqual(c1.Weight, c2.Weight);
        }

        [TestMethod]
        public void Travel()
        {
            Corridor sut = new Corridor("0 1 0.25");

            double result = sut.Travel(8);

            // Console.WriteLine(result);
            Assert.AreEqual(2.0, result);
        }

        [TestMethod]
        public void TravelThroughTwoCorridors()
        {
            Corridor c1 = new Corridor("0 1 0.9");
            Corridor c2 = new Corridor("1 2 0.9");

            double trip1 = c1.Travel(1);
            double trip2 = c2.Travel(trip1);

            Assert.AreEqual(0.81, trip2);
        }

        [TestMethod]
        public void Contains()
        {
            Corridor sut = new Corridor(5, 10, 0.8);

            Assert.IsTrue(sut.Contains(5));
            Assert.IsTrue(sut.Contains(10));
        }
    }
}