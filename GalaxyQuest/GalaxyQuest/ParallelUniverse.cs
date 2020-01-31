using System;
using System.Collections.Generic;
using System.Text;

namespace GalaxyQuest
{
    public class ParallelUniverse
    {
        private readonly List<Coordinate> stars;
        private readonly int distance;

        public ParallelUniverse(IList<Coordinate> stars, int distance)
        {
            this.stars = (List<Coordinate>)stars;
            this.distance = distance;
        }

        public string FindMajority()
        {
            Coordinate champion = stars[0];
            int count = 1;
            foreach (Coordinate star in stars)
            {
                // The first star is chosen as champion
                if (star == champion)
                    continue;

                // If count is at 0, a new champion needs to be picked.
                if (count == 0)
                {
                    champion = star;
                    count++;
                    continue;
                }

                // See if the new star is in range, if so increase count.
                // If not, decrease count.
                if (star.IsWithinRange(champion, distance))
                    count++;
                else
                    count--;
            }

            if (count == 0)
                return "NO";
            else
                return PossibleCandidate(champion);
        }

        private string PossibleCandidate(Coordinate y)
        {
            int count = 0;
            foreach (Coordinate star in stars)
            {
                if (star.IsWithinRange(y, distance))
                    count++;
            }

            if (count > stars.Count / 2)
                return count.ToString();
            return "NO";
        }
    }
}
