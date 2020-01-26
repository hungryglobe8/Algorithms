using CeilingProblem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CeilingProblemTester
{
    [TestClass]
    public class CountUniqueTester
    {
        private CountUnique sut;

        [TestMethod]
        public void Initialize()
        {
            sut = new CountUnique();
            Assert.IsNotNull(sut);
        }

        [TestInitialize]
        public void Setup()
        {
            sut = new CountUnique();
        }

        [TestMethod]
        public void ThreeElements()
        {
            //sut.CountDuplicateStructures();
        }

        private IList<int> MakeSortedList(int numElements)
        {
            IList<int> values = new List<int>();
            for (int i = 0; i < numElements; i++)
            {
                values.Add(i);
            }
            return values;
        }

        private IList<int> MakeRandomList(int seed, int numElements)
        {
            Random r = new Random(seed);
            IList<int> values = new List<int>();
            for (int i = 0; i < numElements; i++)
            {
                values.Add(r.Next());
            }
            return values;
        }
    }
}