using System;
using System.Collections.Generic;

namespace Cartesia_Prime
{
    public class InputReader
    {
        public EscapeRoute GetInput()
        {       
            var start = ReadCoordinateFromLine(Console.ReadLine());
            var transporter = ReadTransporterFromLine(Console.ReadLine());
            var maxMinutes = int.Parse(Console.ReadLine());

            var droneCoords = new List<Coordinate>();
            int numDrones = int.Parse(Console.ReadLine());
            for (int i = 0; i < numDrones; i++)
            {
                droneCoords.Add(ReadCoordinateFromLine(Console.ReadLine()));
            }

            return new EscapeRoute(transporter, maxMinutes, start, droneCoords);
        }

        private Coordinate ReadCoordinateFromLine(string line)
        {
            string[] tokens = line.Split(' ');
            int x = int.Parse(tokens[0]);
            int y = int.Parse(tokens[1]);
            return new Coordinate(x, y);
        }

        private Transporter ReadTransporterFromLine(string line)
        {
            string[] tokens = line.Split(' ');
            int a = int.Parse(tokens[0]);
            int b = int.Parse(tokens[1]);
            int c = int.Parse(tokens[2]);
            int d = int.Parse(tokens[3]);
            return new Transporter(a, b, c, d);
        }
    }
}