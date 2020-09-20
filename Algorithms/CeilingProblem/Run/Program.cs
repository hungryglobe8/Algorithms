using CeilingProblem;
using System;
using System.Collections.Generic;

namespace Run
{
    class Program
    {
        static void Main()
        {
            IList<BinaryTree> availableStructures = new List<BinaryTree>();

            IList<IList<int>> prototypes = GenerateRandomLists(5, 30);
            foreach (IList<int> numbers in prototypes)
            {
                BinaryTree b = new BinaryTree(numbers);
                b.SortTree();
                availableStructures.Add(b);
            }
            int uniqueStructures = CountDuplicateStructures(availableStructures);
            Console.WriteLine(uniqueStructures);
            Console.Read();
        }

        private static IList<IList<int>> GenerateRandomLists(int numLists, int numElements)
        {
            Random r = new Random(1);
            IList<IList<int>> outer = new List<IList<int>>();
            for (int i = 0; i < numLists; i++)
            {
                IList<int> inner = new List<int>();
                for (int j = 0; j < numElements; j++)
                {
                    inner.Add(r.Next(1, 1000000));
                }
                outer.Add(inner);
                foreach (int num in inner)
                {
                    Console.Write(num + " ");
                }
                Console.WriteLine();
            }
            return outer;
        }

        private static int CountDuplicateStructures(IList<BinaryTree> availableStructures)
        {
            // Set to store unique binary tree structures.
            IList<BinaryTree> uniquePatterns = new List<BinaryTree>();

            // Go through availableStructures to see if their indeces match with each other.
            foreach (BinaryTree structure in availableStructures)
            {
                PutTreeInSet(structure, uniquePatterns);
            }
            return uniquePatterns.Count;
        }

        /// <summary>
        /// If a structure has not already been added to uniquePatterns, adds it.
        /// Else, returns.
        /// </summary>
        private static void PutTreeInSet(BinaryTree newTree, IList<BinaryTree> uniqueTrees)
        {
            ISet<int> newPattern = newTree.GetFilledIndeces();
            foreach (BinaryTree tree in uniqueTrees)
            {
                ISet<int> uniquePattern = tree.GetFilledIndeces();
                if (newPattern.SetEquals(uniquePattern))
                    return;
            }
            uniqueTrees.Add(newTree);
        }
    }
}
