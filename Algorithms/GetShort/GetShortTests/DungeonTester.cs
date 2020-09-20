using GetShort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GetShortTests
{
    [TestClass]
    public class DungeonTester
    {
        [TestMethod]
        public void Init()
        {
            Dungeon sut = new Dungeon(GenerateCorridors(5, 1));

            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void SinglePath()
        {
            Dungeon sut = new Dungeon(GenerateCorridors(5, 1));

            double result = sut.Dijkstra();

            Assert.AreEqual(0.0065, result);
        }

        [TestMethod]
        public void VeryLongPath()
        {
            IList<Corridor> corridors = GenerateCorridors(5000, 1);
            corridors.Add(new Corridor(1, 5000, 0.75));
            Dungeon sut = new Dungeon(corridors);

            double result = sut.Dijkstra();

            Assert.AreEqual(0.1865, result);
        }

        [TestMethod]
        public void RandomPaths()
        {
            Dungeon sut = new Dungeon(UpToTenPaths(20, 1));

            double result = sut.Dijkstra();

            Assert.AreEqual(0.5896, result);
        }

        [TestMethod]
        public void LargeRandomPaths()
        {
            for (int i = 0; i < 500; i++)
            {
                Dungeon sut = new Dungeon(UpToTenPaths(500, i));

                double result = sut.Dijkstra();

                Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void LargeBackwardsRandomPaths()
        {
            for (int i = 0; i < 500; i++)
            {
                Dungeon sut = new Dungeon(UpToTenReversePaths(500, i));

                double result = sut.Dijkstra();

                Console.WriteLine($"{i} {result}");
            }
        }

        [TestMethod]
        public void DenseGraph()
        {
            for (int i = 5; i < 100; i++)
            {
                Dungeon sut = new Dungeon(DenseGraph(i, 1));

                double result = sut.Dijkstra();

                Console.WriteLine($"{i} {result}");
            }
        }

        private IList<Corridor> UpToTenReversePaths(int length, int seed)
        {
            Random r = new Random(seed);
            IList<Corridor> corridors = new List<Corridor>();

            for (int i = length - 1; i >= 0; i--)
            {
                int numPaths = r.Next(1, 10);
                for (int j = 0; j < numPaths; j++)
                {
                    int end = r.Next(0, length);
                    if (i != end)
                        corridors.Add(new Corridor(i, end, r.NextDouble() * 90 / 100));
                }
            }

            return corridors;
        }

        private IList<Corridor> DenseGraph(int length, int seed)
        {
            Random r = new Random(seed);
            IList<Corridor> corridors = new List<Corridor>();

            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    corridors.Add(new Corridor(i, j, r.NextDouble() * 90 / 100));
                }
            }

            return corridors;
        }

        private IList<Corridor> GenerateCorridors(int length, int seed)
        {
            Random r = new Random(seed);
            IList<Corridor> corridors = new List<Corridor>();

            for (int i = 0; i < length; i++)
            {
                corridors.Add(new Corridor(i, i + 1, r.NextDouble()));
            }

            return corridors;
        }

        private IList<Corridor> UpToTenPaths(int length, int seed)
        {
            Random r = new Random(seed);
            IList<Corridor> corridors = new List<Corridor>();

            for (int i = 0; i < length; i++)
            {
                int numPaths = r.Next(1, 10);
                for (int j = 0; j < numPaths; j++)
                {
                    int end = r.Next(0, length);
                    if (i != end)
                        corridors.Add(new Corridor(i, end, r.NextDouble() * 90 / 100));
                }
            }

            return corridors;
        }
    }
}