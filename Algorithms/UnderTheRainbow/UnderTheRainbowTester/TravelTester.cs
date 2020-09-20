using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnderTheRainbow;

namespace UnitTests
{
    [TestClass]
    public class TravelTester
    {
        private Travel sut;
        [TestMethod]
        public void Initialize()
        {
            sut = new Travel(BasicInput1());

            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void DistanceBetweenTwoHotels()
        {
            sut = new Travel(BasicInput1());

            int result = sut.MinPenalty(2);

            Assert.AreEqual(625, result);
        }

        [TestMethod]
        public void DistanceBetweenThreeHotels()
        {
            sut = new Travel(BasicInput1());

            int result = sut.MinPenalty(2);

            Assert.AreEqual(625, result);
        }

        [TestMethod]
        public void BasicTest1()
        {
            sut = new Travel(BasicInput1());

            int result = sut.MinPenalty(0);

            Assert.AreEqual(3125, result);
        }

        [TestMethod]
        public void BasicTest2()
        {
            sut = new Travel(BasicInput2());

            int result = sut.MinPenalty(0);

            Assert.AreEqual(5000, result);
        }

        private int[] BasicInput1()
        {
            return new int[] { 0, 350, 450, 825 };
        }

        private int[] BasicInput2()
        {
            return new int[] { 0, 350, 450, 700 };
        }
    }
}
