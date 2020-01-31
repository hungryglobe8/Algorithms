using System;
using System.Collections.Generic;

namespace GalaxyQuest
{
    /// <summary>
    /// First line of input should be integers galactic diameter followed by star count.
    /// Following lines should match number of star count and be written in the same format:
    /// two integers separated by a space. Can be accessed with getters.
    /// </summary>
    public class InputReader : IReader
    {
        private int diameter;
        private List<Coordinate> starList;

        /// <summary>
        /// Reads all user input to member variables.
        /// </summary>
        public void ReadInput()
        {
            string firstLine = Console.ReadLine();
            Tuple<int, int> firstLineTokens = ConvertStringToPairs(firstLine);
            diameter = firstLineTokens.Item1;
            int numStars = firstLineTokens.Item2;
            ReadStarsToList(numStars);
        }

        /// <summary>
        /// Reads remaining user input as stars (coordinates).
        /// Adds each pair as a new Coordinate.
        /// </summary>
        /// <param name="numStars"></param>
        private void ReadStarsToList(int numStars)
        {
            starList = new List<Coordinate>();
            for (int i = 0; i < numStars; i++)
            {
                string line = Console.ReadLine();
                Tuple<int, int> coordinates = ConvertStringToPairs(line);
                Coordinate star = new Coordinate(coordinates.Item1, coordinates.Item2);
                starList.Add(star);
            }
        }

        /// <summary>
        /// Parses first two integers from a string.
        /// </summary>
        /// <param name="line">two integers separated by a space</param>
        /// <returns>first two integers as a tuple</returns>
        private Tuple<int, int> ConvertStringToPairs(string line)
        {
            string[] firstLineTokens = line.Split(' ');
            int item1 = int.Parse(firstLineTokens[0]);
            int item2 = int.Parse(firstLineTokens[1]);
            return new Tuple<int, int>(item1, item2);
        }

        public IList<Coordinate> GetStars()
        {
            return starList;
        }

        public int GetDiameter()
        {
            return diameter;
        }
    }
}
