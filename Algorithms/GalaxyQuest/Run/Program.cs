using GalaxyQuest;
using System;
using System.Collections.Generic;

namespace Run
{
    class Program
    {
        static void Main(string[] args)
        {
            InputReader reader = new InputReader();
            reader.ReadInput();

            int diameter = reader.GetDiameter();
            IList<Coordinate> stars = reader.GetStars();

            ParallelUniverse program = new ParallelUniverse(stars, diameter);
            Console.WriteLine(program.FindMajority());
        }
    }
}
