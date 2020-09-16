using System;

namespace GetShort
{
    public class Corridor
    {
        public int Start { get; private set; }
        public int End { get; private set; }
        public double Weight { get; private set; }
        public bool Contains(int edge) => Start == edge || End == edge;

        public Corridor(int start, int end, double sentry)
        {
            if (start < end)
            {
                Start = start;
                End = end; 
            }
            else
            {
                End = start;
                Start = end;
            }
            Weight = sentry;
        }

        public Corridor(string line)
        {
            string[] tokens = line.Split(' ');
            int start = int.Parse(tokens[0]);
            int end = int.Parse(tokens[1]);
            Weight = double.Parse(tokens[2]);

            if (start < end)
            {
                Start = start;
                End = end;
            }
            else
            {
                End = start;
                Start = end;
            }
        }

        /// <summary>
        /// Returns a double representing the cost of travelling through a corridor.
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public double Travel(double original)
        {
            // Round to four digits or truncate?
            original = Math.Round(original, 4);

            return Math.Round(original * Weight, 4);
        }

        public override string ToString()
        {
            return $"{Start} -> {End}: {Weight}";
        }
    }
}
