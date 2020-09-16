using System;
using System.Collections.Generic;
using System.Text;

namespace Fireworks
{
    class InputReader
    {
        public IList<int> GetInput(Launchers launchers)
        {
            string[] tokens = Console.ReadLine().Split(' ');
            int numLaunchers = int.Parse(tokens[0]);
            int numScenarios = int.Parse(tokens[1]);
            for (int i = 0; i < numLaunchers - 1; i++)
            {
                launchers.AddDistance(int.Parse(Console.ReadLine()));
            }

            IList<int> numShells = new List<int>();
            for (int i = 0; i < numScenarios; i++)
            {
                numShells.Add(int.Parse(Console.ReadLine()));
            }
            return numShells;
        }
    }
}
