using AutoSink;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class MapTester
    {
        [TestMethod]
        public void Init()
        {
            Map sut = new Map(SampleInput1());

            Assert.IsNotNull(sut);
        }

        private IList<City> SampleInput1()
        {
            City c1 = new City("Sourceville", 5);
            City c2 = new City("SinkCity", 10);
            City c3 = new City("Easton", 20);
            City c4 = new City("Weston", 15);
            c1.AddHighway(c4);
            c1.AddHighway(c3);
            c4.AddHighway(c2);
            c3.AddHighway(c2);

            return new List<City>() { c4, c2, c1, c3 };
        }

        [TestMethod]
        public void TopologicalSort()
        {
            Map sut = new Map(SampleInput1());

            sut.TopologicalSort();
            IList<City> result = sut.GetSortedCities();

            Assert.AreEqual("Sourceville", result[0].Name);
        }

        [TestMethod]
        public void TopologicalAF()
        {
            Map sut = new Map(BasicInput1());

            sut.TopologicalSort();
            IList<City> result = sut.GetSortedCities();

            Assert.AreEqual("A", result[0].Name);
        }

        [TestMethod]
        public void NumPathsAF()
        {
            Map sut = new Map(BasicInput1());

            sut.TopologicalSort();
            IList<City> sorted = sut.GetSortedCities();
            City A = sorted[0];
            City D = sorted[1];
            City C = sorted[4];
            City F = sorted[6];

            int result1 = sut.FindNumPaths(A, F);
            Assert.AreEqual(6, result1);

            int result2 = sut.FindNumPaths(D, C);
            Assert.AreEqual(2, result2);

            int result3 = sut.FindNumPaths(D, F);
            Assert.AreEqual(4, result3);
        }

        [TestMethod]
        public void CheapTripSampleInput1()
        {
            Map sut = new Map(SampleInput1());

            sut.TopologicalSort();
            IList<City> sorted = sut.GetSortedCities();
            City source = sorted[0];
            City east = sorted[1];
            City west = sorted[2];
            City sink = sorted[3];

            string result1 = sut.FindCheapestPath(source, sink);
            Assert.AreEqual("25", result1);

            string result2 = sut.FindCheapestPath(east, sink);
            Assert.AreEqual("10", result2);

            string result3 = sut.FindCheapestPath(sink, sink);
            Assert.AreEqual("0", result3);

            string result4 = sut.FindCheapestPath(west, west);
            Assert.AreEqual("0", result4);

            string result5 = sut.FindCheapestPath(west, source);
            Assert.AreEqual("NO", result5);

            string result6 = sut.FindCheapestPath(sink, source);
            Assert.AreEqual("NO", result6);
        }

        [TestMethod]
        public void CheapTripSampleInput2()
        {
            Map sut = new Map(BasicInput2());

            sut.TopologicalSort();
            IList<City> sorted = sut.GetSortedCities();
            City B = sorted[0];
            City E = sorted[1];
            City A = sorted[2];
            City D = sorted[3];
            City C = sorted[4];
            City G = sorted[5];
            City F = sorted[6];

            string result1 = sut.FindCheapestPath(A, F);
            Assert.AreEqual("25", result1);

            string result2 = sut.FindCheapestPath(B, G);
            Assert.AreEqual("35", result2);

            string result3 = sut.FindCheapestPath(A, C);
            Assert.AreEqual("15", result3);
        }

        [TestMethod]
        public void CheapTripSampleInput3()
        {
            Map sut = new Map(BasicInput3());

            sut.TopologicalSort();
            IList<City> sorted = sut.GetSortedCities();
            City A = sorted[0];
            City B = sorted[1];
            City D = sorted[2];
            City C = sorted[3];

            string result1 = sut.FindCheapestPath(A, D);
            Assert.AreEqual("15", result1);

            string result2 = sut.FindCheapestPath(A, C);
            Assert.AreEqual("6", result2);
        }

        [TestMethod]
        public void CheapTripSampleInput4()
        {
            Map sut = new Map(BasicInput4());

            sut.TopologicalSort();
            IList<City> sorted = sut.GetSortedCities();
            City A = sorted[0];
            City B = sorted[1];
            City E = sorted[2];
            City C = sorted[3];
            City D = sorted[4];

            string result1 = sut.FindCheapestPath(A, D);
            Assert.AreEqual("15", result1);

            string result2 = sut.FindCheapestPath(E, B);
            Assert.AreEqual("NO", result2);
        }

        [TestMethod]
        public void CheapTripAdvanced()
        {
            Map sut = new Map(Advanced());

            sut.TopologicalSort();
            IList<City> sorted = sut.GetSortedCities();
            City A = sorted[0];
            City B = sorted[1];
            City D = sorted[2];
            City C = sorted[3];
            City F = sorted[4];
            City E = sorted[5];

            string result1 = sut.FindCheapestPath(A, E);
            Assert.AreEqual("12", result1);

            string result2 = sut.FindCheapestPath(A, F);
            Assert.AreEqual("10", result2);
        }

        private IList<City> BasicInput1()
        {
            City A = new City("A", 5);
            City B = new City("B", 10);
            City C = new City("C", 20);
            City D = new City("D", 15);
            City E = new City("E", 10);
            City G = new City("G", 20);
            City F = new City("F", 5);

            A.AddHighway(B);
            A.AddHighway(D);
            B.AddHighway(E);
            D.AddHighway(B);
            D.AddHighway(E);
            E.AddHighway(C);
            C.AddHighway(G);
            E.AddHighway(G);
            G.AddHighway(F);
            
            return new List<City>() { A, B, C, D, E, G, F };
        }

        private IList<City> BasicInput2()
        {
            City A = new City("A", 5);
            City B = new City("B", 10);
            City C = new City("C", 15);
            City D = new City("D", 10);
            City E = new City("E", 20);
            City F = new City("F", 10);
            City G = new City("G", 20);

            A.AddHighway(C);
            A.AddHighway(D);
            B.AddHighway(C);
            B.AddHighway(E);
            C.AddHighway(F);
            C.AddHighway(G);
            D.AddHighway(G);
            E.AddHighway(G);

            return new List<City>() { A, B, C, D, E, F, G };
        }

        private IList<City> BasicInput3()
        {
            City A = new City("A", 10);
            City B = new City("B", 5);
            City C = new City("C", 1);
            City D = new City("D", 10);

            A.AddHighway(B);
            B.AddHighway(C);
            B.AddHighway(D);

            return new List<City>() { A, B, C, D };
        }

        private IList<City> BasicInput4()
        {
            City A = new City("A", 30);
            City B = new City("B", 5);
            City C = new City("C", 5);
            City D = new City("D", 10);
            City E = new City("E", 5);

            A.AddHighway(B);
            A.AddHighway(E);
            B.AddHighway(C);
            C.AddHighway(D);
            E.AddHighway(D);

            return new List<City>() { C, E, D, B, A };
        }

        private IList<City> Advanced()
        {
            City A = new City("A", 30);
            City B = new City("B", 5);
            City C = new City("C", 6);
            City D = new City("D", 7);
            City E = new City("E", 7);
            City F = new City("F", 10);

            A.AddHighway(B);
            A.AddHighway(C);
            A.AddHighway(D);
            B.AddHighway(E);
            C.AddHighway(E);
            D.AddHighway(E);
            C.AddHighway(F);
            D.AddHighway(F);
            A.AddHighway(F);

            return new List<City>() { C, E, D, B, A, F };
        }
    }
}
