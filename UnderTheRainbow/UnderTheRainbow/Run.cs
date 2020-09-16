using System;
using System.Collections.Generic;

namespace UnderTheRainbow
{
    public class Run
    {
        /// <summary>
        /// Gets input, stores it in distances.
        /// Makes a new Travel object with distances and gets the minPenalty from start.
        /// </summary>
        public static void Main()
        {
            InputReader reader = new InputReader();
            int[] distances = reader.Distances;
            Travel travel = new Travel(distances);
            int minPenalty = travel.MinPenalty(0);
            Console.WriteLine(minPenalty);
        }
    }
}
