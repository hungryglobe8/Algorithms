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
            IList<BinaryTree> prototypes = MakeBinaryTrees(10, 3);
            int result = CountUnique.CountDuplicateStructures(prototypes);
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void MaxElements()
        {
            IList<BinaryTree> prototypes = MakeBinaryTrees(50, 40);
            int result = CountUnique.CountDuplicateStructures(prototypes);
            Console.WriteLine(result);
        }

        private IList<BinaryTree> MakeBinaryTrees(int numTrees, int numElements)
        {
            IList<BinaryTree> trees = new List<BinaryTree>();
            for (int i = 0; i < numTrees; i++)
            {
                IList<int> numbers = MakeRandomList(numElements);
                BinaryTree tree = new BinaryTree(numbers);
                tree.SortTree();
                trees.Add(tree);
            }
            return trees;
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

        private IList<int> MakeRandomList(int numElements)
        {
            Random r = new Random();
            IList<int> values = new List<int>();
            for (int i = 0; i < numElements; i++)
            {
                values.Add(r.Next(1, 100000));
            }
            return values;
        }
    }
}