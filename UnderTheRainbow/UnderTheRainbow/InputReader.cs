using System;
using System.Collections.Generic;
using System.Linq;

namespace UnderTheRainbow
{
    /// <summary>
    /// First line of input is 1 <= n <= 1000, the number of hotels minus the starting one.
    /// Next n + 1 lines give the elements of the hotel distance array. Last element is less than 30,000.
    /// </summary>
    public class InputReader
    {
        public int[] Distances { private set; get; }

        /// <summary>
        /// Reads all user input to member variables.
        /// </summary>
        public InputReader()
        {
            // first section (1 <= n <= 1000)
            int numHotels = int.Parse(Console.ReadLine()) + 1;
            Distances = new int[numHotels];

            // read distances
            for (int i = 0; i < numHotels; i++)
            {
                int distance = int.Parse(Console.ReadLine());
                Distances[i] = distance;
            }
        }
    }
}