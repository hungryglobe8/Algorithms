using AutoSink;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class CityTester
    {
        [TestMethod]
        public void Init()
        {
            City sut = new City("Sourceville", 5);

            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void AddHighwayBetweenTwoCities()
        {
            City c1 = new City("Sourceville", 10);
            City c2 = new City("Easton", 15);

            c1.AddHighway(c2);
            int result = c1.GetHighways().Count;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void AddManyHighwaysFromOneCity()
        {
            City c1 = new City("Sourceville", 10);
            City c2 = new City("Easton", 15);
            City c3 = new City("Scottsdale", 5);
            City c4 = new City("Brunning", 20);

            c1.AddHighway(c2);
            c1.AddHighway(c3);
            c1.AddHighway(c4);

            Assert.AreEqual(3, c1.GetHighways().Count);
            Assert.AreEqual(3, c1.OutgoingRoads);
        }
    }
}
