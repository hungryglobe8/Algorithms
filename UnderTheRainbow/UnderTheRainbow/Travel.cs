using System;
using System.Collections.Generic;
using System.Linq;

namespace UnderTheRainbow
{
    public class Travel
    {
        public int[] _distances;
        private int MAX_PENALTY = int.MaxValue;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        public Travel(int[] distances)
        {
            _distances = distances;
        }

        public int MinPenalty(int index)
        {
            return MinPenalty(new Dictionary<int, int> (), index);
        }

        private int MinPenalty(Dictionary<int, int> cache, int i)
        {
            // Value has been found before
            int result;
            if (cache.TryGetValue(i, out result))
                return result;
            
            // Base case
            if (i == _distances.Length - 1)
                return 0;

            // Recursive part
            int minPenalty = MAX_PENALTY;
            for (int k = i + 1; k < _distances.Length; k++)
            {
                int penalty = (int) Math.Pow(400 - (_distances[k] - _distances[i]), 2) + MinPenalty(cache, k);
                minPenalty = Math.Min(minPenalty, penalty);
            }

            cache[i] = minPenalty;
            return minPenalty;
        }
    }
}