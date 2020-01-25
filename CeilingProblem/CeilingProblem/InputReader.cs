using System;
using System.Collections.Generic;
using System.Text;

namespace CeilingProblem
{
    public class InputReader
    {
        public IList<IList<int>> GetUserInput()
        {
            string firstLine = Console.ReadLine();
            IList<int> firstLineTokens = ConvertStringToIntegers(firstLine);
            int numPrototypes = firstLineTokens[0];
            return ReadPrototypes(numPrototypes);
        }

        private IList<IList<int>> ReadPrototypes(int numPrototypes)
        {
            IList<IList<int>> prototypes = new List<IList<int>>();
            for (int i = 0; i < numPrototypes; i++)
            {
                string line = Console.ReadLine();
                IList<int> integers = ConvertStringToIntegers(line);
                prototypes.Add(integers);
            }
            return prototypes;
        }

        /// <summary>
        /// Convert a string of ints seperated by spaces into a list of ints.
        /// </summary>
        /// <param name="firstLine">string to be split</param>
        private IList<int> ConvertStringToIntegers(string line)
        {
            string[] firstLineTokens = line.Split(' ');
            IList<int> integers = new List<int>();

            foreach (string s in firstLineTokens)
                integers.Add(int.Parse(s));

            return integers;
        }
    }
}
