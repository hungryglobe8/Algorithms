using CeilingProblem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CeilingProblemTester
{
    [TestClass]
    public class BinaryTreeTester
    {
        [TestMethod]
        public void Initialize()
        {
            BinaryTree sut = new BinaryTree(new List<int>() { });
            Assert.IsNotNull(sut);
        }

        [DataTestMethod]
        [DataRow(1, 2, 3)]
        [DataRow(2, 1, 3)]
        [DataRow(3, 2, 1)]
        public void FirstElementIsAtZeroIndex(int a, int b, int c)
        {
            IList<int> numbers = new List<int>() { a, b, c };
            BinaryTree sut = new BinaryTree(numbers);

            sut.SortTree();
            int[] storage = sut.GetStorage();

            Assert.AreEqual(a, storage[0]);
        }

        [TestMethod]
        public void SameStructure_3Elements()
        {
            IList<int> numbers1 = new List<int>() { 2, 3, 1 };
            IList<int> numbers2 = new List<int>() { 2, 1, 3 };
            BinaryTree sut1 = new BinaryTree(numbers1);
            BinaryTree sut2 = new BinaryTree(numbers2);

            sut1.SortTree();
            sut2.SortTree();
            ISet<int> indeces1 = sut1.GetFilledIndeces();
            ISet<int> indeces2 = sut2.GetFilledIndeces();

            Assert.IsTrue(indeces1.SetEquals(indeces2));
        }

        [TestMethod]
        public void DiffStructure_3Elements()
        {
            IList<int> numbers1 = new List<int>() { 2, 3, 1 };
            IList<int> numbers2 = new List<int>() { 1, 2, 3 };
            BinaryTree sut1 = new BinaryTree(numbers1);
            BinaryTree sut2 = new BinaryTree(numbers2);

            sut1.SortTree();
            sut2.SortTree();
            ISet<int> indeces1 = sut1.GetFilledIndeces();
            ISet<int> indeces2 = sut2.GetFilledIndeces();

            Assert.IsFalse(indeces1.SetEquals(indeces2));
        }

        [TestMethod]
        public void ArrayCanSupportUpTo20Layers()
        {
            IList<int> numbers = MakeSortedList(30);
            BinaryTree sut = new BinaryTree(numbers);

            sut.SortTree();
            Assert.IsNotNull(sut.GetStorage());
        }

        [TestMethod]
        public void BigTree()
        {
            IList<int> numbers = MakeRandomList(5, 50);
            BinaryTree sut = new BinaryTree(numbers);

            sut.SortTree();
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
