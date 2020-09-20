using System;
using System.Collections.Generic;
using System.Linq;

namespace GetShort
{
    /// <summary>
    /// Input consists of multiple test cases. Final test has n = m = 0. 
    /// Each test has two variable n and m. 
    /// 2 <= n <= 10000 is the number of intersections.
    /// 1 <= m <= 15000 is the number of corridors.
    /// m lines follow containing two integers (for corridor endpoints) and a weapon factor.
    /// </summary>
    public class InputReader
    {
        public IList<Corridor> corridors = new List<Corridor>();
        /// <summary>
        /// Reads all user input to member variables.
        /// </summary>
        public void ReadInput()
        {
            string line = Console.ReadLine();
            Tuple<int, int> nums = ConvertStringToTest(line);
            int n = nums.Item1;
            int m = nums.Item2;

            while (n != 0 && m != 0)
            {
                // Load m corridors
                for (int i = 0; i < m; i++)
                {
                    line = Console.ReadLine();
                    corridors.Add(new Corridor(line));
                }
                // Make a dungeon and run algorithm
                Dungeon dungeon = new Dungeon(corridors);
                double result = dungeon.Dijkstra();
                PrintFourZeros(result);

                // Get next test ready
                line = Console.ReadLine();
                nums = ConvertStringToTest(line);
                n = nums.Item1;
                m = nums.Item2;
                corridors = new List<Corridor>();
            }
        }

        private void PrintFourZeros(double result)
        {
            if (result == 1)
                Console.WriteLine("1.0000");

            else
            {
                string line = result.ToString();
                while (line.Length != 6)
                {
                    line += '0';
                }
                Console.WriteLine(line);
            }
        }

        /// <summary>
        /// Convert a string to a tuple of ints.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private Tuple<int, int> ConvertStringToTest(string line)
        {
            string[] tokens = line.Split(' ');

            return new Tuple<int, int>(int.Parse(tokens[0]), int.Parse(tokens[1]));
        }

        public IList<Corridor> GetCorridors()
        {
            return corridors;
        }
    }
}
