using System;
using System.Collections.Generic;
using System.Text;

namespace Cartesia_Prime
{
    public static class Run
    {
        public static void Main()
        {
            Grid grid = new Grid();

            InputReader r = new InputReader(grid);
            Transporter trans = r.Transporter;
            int m = r.M;
            Coordinate start = r.Start;

            int result = grid.ShortestPath(trans, m, start);

            PrintOutput(result);
        }

        private static void PrintOutput(int result)
        {
            if (result == -1)
                Console.WriteLine("You will be assimilated! Resistance is futile!");
            else
                Console.WriteLine($"I had {result} to spare! Beam me up Scotty!");
        }
    }
}
