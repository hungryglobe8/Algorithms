using System;

namespace Cartesia_Prime
{
    public class InputReader
    {
        public Transporter Transporter { get; internal set; }
        public int M { get; internal set; }
        public Coordinate Start { get; internal set; }

        public InputReader(Grid grid)
        {
            Start = ReadCoordinateFromLine(Console.ReadLine());
            Transporter = ReadTransporterFromLine(Console.ReadLine());
            M = int.Parse(Console.ReadLine());
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] tokens = Console.ReadLine().Split(' ');
                int x = int.Parse(tokens[0]);
                int y = int.Parse(tokens[1]);
                grid.AddDrone(x, y);
            }
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