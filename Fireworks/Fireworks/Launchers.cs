using System;
using System.Collections.Generic;
using System.Linq;

namespace Fireworks
{
    public class Launchers
    {
        private IList<int> distances;
        private int numLaunchers;
        private int minDistance;

        public Launchers()
        {
            distances = new List<int>();
            numLaunchers = 1;
            minDistance = 1;
        }

        public Launchers(IList<int> _distances)
        {
            distances = _distances;
            numLaunchers = _distances.Count + 1;
            minDistance = _distances.Min();
        }

        public void AddDistance(int distance)
        {
            distances.Add(distance);
            if (distance < minDistance)
                minDistance = distance;
            numLaunchers++;
        }

        /// <summary>
        /// Given a number of shells to be launched at a given moment,
        /// returns the optimal smallest separation.
        /// </summary>
        /// <param name="numShells"> (2 <= numShells <= numLaunchers) </param>
        /// <returns></returns>
        public int LauncherDistance(int min, int numShells)
        {
            int SWITCH_TO_LINEAR = 10;
            int low = min;

            int high = low * 2;
            while (true)
            {
                if (LaunchIsPossible(high, numShells))
                {
                    low = high;
                    high *= 2;
                    continue;
                }
                else
                    high = (low + high) / 2;

                if (high - low < SWITCH_TO_LINEAR)
                    break;
            }
            return NextLinearLaunch(low, numShells);
        }

        private int NextLinearLaunch(int distance, int numShells)
        {
            while (LaunchIsPossible(distance, numShells))
            {
                distance++;
            }
            return distance - 1;
        }

        /// <summary>
        /// Decision algorithm determines if it is possible for a given set of launchers
        /// to make a launch with at least distance d between all launchers.
        /// </summary>
        /// <param name="launch"></param>
        /// <param name="dist"></param>
        /// <param name="numShells"></param>
        public bool LaunchIsPossible(int dist, int numShells)
        {
            int count = 1;
            int distanceBetweenLaunchers = 0;
            foreach (int val in distances)
            {
                distanceBetweenLaunchers += val;
                if (distanceBetweenLaunchers >= dist)
                {
                    distanceBetweenLaunchers = 0;
                    count++;
                }

                if (count >= numShells)
                    return true;
            }
            return false;
        }
    }
}
