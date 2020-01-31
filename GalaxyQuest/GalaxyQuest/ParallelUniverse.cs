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
            this.stars = (List<Coordinate>) stars;
            this.distance = distance;
        }

        public string FindMajority()
        {
            return FindMajority(stars);
        }

        private string FindMajority(List<Coordinate> list)
        {
            int length = list.Count;
            if (length == 0)
            {
                return null;
            }
            else if (length == 1)
            {
                return list[0];
            }
            else
            {
                int midIndex = length / 2;
                Coordinate x = FindMajority(list.GetRange(0, length / 2));
                Coordinate y = FindMajority(midIndex + 1, length / 2));
                if (x == "NO" && y == "NO")
                    return "NO";
                else if (x == "NO")
                    return PossibleCandidate(y);
            }
            throw new NotImplementedException();
        }

        private string PossibleCandidate(Coordinate y, List<Coordinate> list)
        {
            int count = 0;
            foreach (Coordinate star in list)
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
