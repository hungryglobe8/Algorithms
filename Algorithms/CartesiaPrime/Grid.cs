using System;
using System.Collections.Generic;
using System.Text;

namespace Cartesia_Prime
{
    public class Grid
    {
        private IList<Coordinate> drones;
        private Coordinate goal;

        public Grid()
        {
            drones = new List<Coordinate>();
            goal = new Coordinate(0, 0);
        }

        public void AddDrone(int x, int y)
        {
            drones.Add(new Coordinate(x, y));
        }

        /// <summary>
        /// Looks for the shortest path possible from a starting coordinate to (0,0).
        /// Returns -1 if the path does not exist.
        /// Else returns the number of steps left before time runs out (m - t).
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="m"></param>
        /// <returns>Number of steps it took to find (0,0)</returns>
        public int ShortestPath(Transporter trans, int m, Coordinate start)
        {
            Queue<Tuple<Coordinate, int>> q = new Queue<Tuple<Coordinate, int>>();
            // Each addition to the queue must keep track of its own time.
            q.Enqueue(new Tuple<Coordinate, int>(start, 1));

            while (q.Count != 0)
            {
                Tuple<Coordinate, int> tuple = q.Dequeue();
                Coordinate curr = tuple.Item1;
                // If a drone is in the same spot as curr, move on to the next possible path.
                if (drones.Contains(curr))
                    continue;
                int t = tuple.Item2;
                // Goal has been reached.
                if (curr.Equals(goal))
                    return m - t + 1;
                // Not worth considering future paths (not enough time)
                if (t > m)
                    continue;

                foreach (Coordinate move in MovementPossibilities(curr, trans, t))
                    q.Enqueue(new Tuple<Coordinate, int>(move, t + 1));
            }
            return -1;
        }

        /// <summary>
        /// Transporter acts to provide magnitude of movement.
        /// Coordinates are added together for 4 new possible coordinates.
        /// </summary>
        /// <param name="curr"></param>
        /// <param name="trans"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private IEnumerable<Coordinate> MovementPossibilities(Coordinate curr, Transporter trans, int t)
        {
            Coordinate magnitude = trans.Move(t);
            return curr + magnitude;
        }
    }
}
