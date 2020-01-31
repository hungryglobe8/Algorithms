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
            ParallelUniverse sut = new ParallelUniverse(new List<Coordinate>());

            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void SmallList()
        {
            ParallelUniverse sut = new ParallelUniverse(GenerateRandomCoordinates(5));

            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void FindMajority_SmallList()
        {
            ParallelUniverse sut = new ParallelUniverse(GenerateListWithMajority(8));

            Console.WriteLine((5 - 0) / 2);

            //Assert.AreEqual(5, result);
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

        private IList<Coordinate> GenerateListWithMajority(int num)
        {
            Random r = new Random(3);
            List<Coordinate> result = new List<Coordinate>();
            for (int i = 0; i < num / 2; i++)
            {
                int x = r.Next(1, 8);
                int y = r.Next(1, 8);
                result.Add(new Coordinate(x, y));
            }
            for (int i = num / 2 + 1; i < num; i++)
            {
                int x = r.Next(50, 57);
                int y = r.Next(50, 57);
                result.Add(new Coordinate(x, y));
            }
            return result;
        }
    }
}

