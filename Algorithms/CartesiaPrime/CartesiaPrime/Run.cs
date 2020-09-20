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

            InputReader r = new InputReader();
            EscapeRoute route = r.GetInput();
            grid.AddDrones(route.DroneCoordinates);
            int result = grid.ShortestPath(route);

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
