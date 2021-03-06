﻿using System;
using System.Collections.Generic;

namespace CeilingProblem
{
    /// <summary>
    /// BinaryTreeCreator takes in an IList of integers as its constructor.
    /// After SortTree is called, all values will be in storage in the format
    /// of a Binary Search Tree. A sorted set of integers is kept to determine if
    /// two trees have a similar structure.
    /// </summary>
    public class BinaryTree
    {
        int[] storage;
        int SIZE = 1024;
        ISet<int> filledIndeces;
        IList<int> unsortedList;

        public BinaryTree(IList<int> numbers)
        {
            storage = new int[SIZE];
            filledIndeces = new SortedSet<int>();
            unsortedList = numbers;
        }

        public void SortTree()
        {
            foreach (int val in unsortedList)
            {
                Insert(val, 0);
            }
        }

        private void Insert(int val, int currentIndex)
        {
            if (currentIndex > SIZE)
            {
                MoveToNewStorage();
            }
            // Base case
            if (storage[currentIndex] == 0)
            {
                storage[currentIndex] = val;
                filledIndeces.Add(currentIndex);
                return;
            }
            // Move val to the left subtree
            else if (val < storage[currentIndex])
            {
                currentIndex = (2 * currentIndex) + 1;
                Insert(val, currentIndex);
            }
            // Move val to the right subtree
            else
            {
                currentIndex = (1 + currentIndex) * 2;
                Insert(val, currentIndex);
            }
        }

        private void MoveToNewStorage()
        {
            SIZE *= 2;
            int[] newStorage = new int[SIZE];
            storage.CopyTo(newStorage, 0);
            storage = newStorage;
        }

        public int[] GetStorage()
        {
            return storage;
        }

        public ISet<int> GetFilledIndeces()
        {
            return filledIndeces;
        }
    }
}
