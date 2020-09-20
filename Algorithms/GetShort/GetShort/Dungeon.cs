using System;
using System.Collections.Generic;
using System.Text;

namespace GetShort
{
    public class Dungeon
    {
        private readonly IDictionary<int, IList<Corridor>> map;
        public int numIntersections, numCorridors = 0;
        private readonly double[] dist;

        public Dungeon(IList<Corridor> corridors)
        {
            map = new Dictionary<int, IList<Corridor>>();
            foreach (Corridor corridor in corridors)
            {
                AddCorridor(corridor);
            }

            numIntersections = map.Keys.Count;
            dist = new double[numIntersections];
        }

        public double Dijkstra()
        {
            for (int i = 0; i < numIntersections; i++)
            {
                dist[i] = 0;
            }
            dist[0] = 1;

            PriorityQueue pq = new PriorityQueue(numIntersections);
            pq.Update(0, 1);

            while (!pq.IsEmpty())
            {
                int u = pq.DeleteMin();

                foreach (Corridor corridor in map[u])
                {
                    int v = corridor.End;
                    double travel = corridor.Travel(dist[u]);
                    if (dist[v] < travel)
                    {
                        dist[v] = travel;
                        pq.Update(v, dist[v]);
                    }
                }
            }

            return dist[numIntersections - 1];
        }

        /// <summary>
        /// Add a corridor to the map depending
        /// if its starting intersection has been added already.
        /// </summary>
        /// <param name="corridor"></param>
        private void AddCorridor(Corridor corridor)
        {
            int start = corridor.Start;
            if (map.ContainsKey(start))
            {
                map[start].Add(corridor);
            }
            else
            {
                map.Add(start, new List<Corridor>() { corridor });
            }

            int end = corridor.End;
            if (!map.ContainsKey(end))
            {
                map.Add(end, new List<Corridor>());
            }
            numCorridors++;
        }
    }
}
