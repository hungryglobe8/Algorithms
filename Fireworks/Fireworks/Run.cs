using System;
using System.Collections.Generic;
using System.Text;

namespace Fireworks
{
    public static class Run
    {
        public static void Main()
        {
            Launchers launchers = new Launchers();
            InputReader reader = new InputReader();

            IList<int> scenarios = reader.GetInput(launchers);
            SaveResultsOptimization(scenarios, launchers);
        }

        public static void SaveResultsOptimization(IList<int> scenarios, Launchers launchers)
        {
            IDictionary<int, int> results = new SortedDictionary<int, int>();
            foreach (var numShells in scenarios)
            {
                int result = 0;
                if (results.TryGetValue(numShells, out result))
                    Console.WriteLine(result);
                int min = FindNearestInDict(numShells, results);
                result = launchers.LauncherDistance(min, numShells);
                Console.WriteLine(result);
                results.Add(numShells, result);
            }
        }

        private static int FindNearestInDict(int numShells, IDictionary<int, int> results)
        {
            ICollection<int> list = results.Keys;
            if (list.Count == 0)
                return 1;

            else
            {
                int curr = 0;
                foreach (var item in list)
                {
                    if (numShells < item)
                        curr = item;
                    // Should be sorted
                    else if (curr == 0)
                        return 1;
                    else
                        break;
                }
                return results[curr];
            }
        }
    }
}
