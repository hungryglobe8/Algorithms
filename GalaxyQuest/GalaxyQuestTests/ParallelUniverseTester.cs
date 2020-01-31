using GalaxyQuest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GalaxyQuestTests
{
    [TestClass]
    public class ParallelUniverseTester
    {
        [TestMethod]
        public void Initialize()
        {
            ParallelUniverse sut = new ParallelUniverse(new List<Coordinate>(), 5);

            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void SmallList()
        {
            ParallelUniverse sut = new ParallelUniverse(GenerateRandomCoordinates(5), 5);

            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void FindMajority_SmallList()
        {
            ParallelUniverse sut = new ParallelUniverse(GenerateListWithMajority(8, 7, 50), 10);

            var result = sut.FindMajority();

            Assert.AreEqual("5", result);
        }

        [TestMethod]
        public void FindMajority_LargeList()
        {
            ParallelUniverse sut = new ParallelUniverse(GenerateListWithMajority(100000, 500, 1500), 708);

            var result = sut.FindMajority();

            Assert.AreEqual("50001", result);
        }

        private IList<Coordinate> GenerateRandomCoordinates(int num)
        {
            Random r = new Random(3);
            List<Coordinate> result = new List<Coordinate>();
            for (int i = 0; i < num; i++)
            {
                int x = r.Next(1, 1000);
                int y = r.Next(1, 1000);
                result.Add(new Coordinate(x, y));
            }
            return result;
        }

        private IList<Coordinate> GenerateListWithMajority(int numCoordinates, int distance, int secondSet)
        {
            Random r = new Random(3);
            List<Coordinate> result = new List<Coordinate>();
            for (int i = 0; i < numCoordinates / 2 + 1; i++)
            {
                int x = r.Next(1, distance);
                int y = r.Next(1, distance);
                result.Add(new Coordinate(x, y));
            }
            for (int i = (numCoordinates + 1) / 2; i < numCoordinates; i++)
            {
                int x = r.Next(secondSet, secondSet + distance);
                int y = r.Next(secondSet, secondSet + distance);
                result.Add(new Coordinate(x, y));
            }
            return result;
        }
    }
}

