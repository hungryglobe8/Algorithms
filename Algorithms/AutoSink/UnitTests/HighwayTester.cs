using AutoSink;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class HighwayTester
    {
        [TestMethod]
        public void Init()
        {
            Highway sut = MakeBasicHighway();

            Assert.IsNotNull(sut);
        }

        private Highway MakeBasicHighway()
        {
            City c1 = new City("A1", 1);
            City c2 = new City("A2", 2);
            return new Highway(c1, c2);
        }

        [TestMethod]
        public void BasicHighwaySourceIsC1()
        {
            Highway sut = MakeBasicHighway();

            City source = sut.Source;
            string actual = source.Name;

            Assert.AreEqual("A1", actual);
        }

        [TestMethod]
        public void BasicHighwayDestinationIsC2()
        {
            Highway sut = MakeBasicHighway();

            City dest = sut.Destination;
            string actual = dest.Name;

            Assert.AreEqual("A2", actual);
        }
    }
}
