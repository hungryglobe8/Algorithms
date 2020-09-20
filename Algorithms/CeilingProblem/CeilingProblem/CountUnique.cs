using System;
using System.Collections.Generic;
using System.Text;

namespace CeilingProblem
{
    public class CountUnique
    {
        public static int Main()
        {
            IList<BinaryTree> availableStructures = new List<BinaryTree>();

            InputReader reader = new InputReader();
            IList<IList<int>> prototypes = reader.GetUserInput();
            foreach (IList<int> numbers in prototypes)
            {
                BinaryTree b = new BinaryTree(numbers);
                b.SortTree();
                availableStructures.Add(b);
            }
            int uniqueStructures = CountDuplicateStructures(availableStructures);
            Console.WriteLine(uniqueStructures);

            return 0;
        }

        public static int CountDuplicateStructures(IList<BinaryTree> availableStructures)
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
            if (uniqueTrees.Count == 0)
            {
                uniqueTrees.Add(newTree);
                return;
            }

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
