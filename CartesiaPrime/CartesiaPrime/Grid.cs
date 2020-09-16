using System;
using System.Collections.Generic;
using System.Text;

namespace Cartesia_Prime
{
    public class Grid
    {
        private IList<Coordinate> drones;
        private Coordinate goal;

        /// <summary>
        /// Initialize goal and list of drones.
        /// </summary>
        public Grid()
        {
            drones = new List<Coordinate>();
            goal = new Coordinate(0, 0);
        }

        /// <summary>
        /// Add a new 'dead zone' to the grid.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddDrone(int x, int y)
        {
            drones.Add(new Coordinate(x, y));
        }

        public void AddDrones(IList<Coordinate> coords)
        {
            foreach(var coord in coords)
            {
                drones.Add(coord);
            }
        }

        /// <summary>
        /// Looks for the shortest path possible from a starting coordinate to (0,0).
        /// Returns -1 if the path does not exist.
        /// Else returns the number of steps left before time runs out (m - t).
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="route"></param>
        /// <returns>Number of steps it took to find (0,0)</returns>
        public int ShortestPath(EscapeRoute route)
        {
            var q = new Queue<CoordInTime>();
            // Each addition to the queue must keep track of its own time.
            q.Enqueue(new CoordInTime(route.Start, 1));

            while (q.Count != 0)
            {
                CoordInTime coordInTime = q.Dequeue();
                Coordinate currCoordinate = coordInTime.Coord;
                // If a drone is in the same spot as curr, move on to the next possible path.
                if (drones.Contains(currCoordinate))
                    continue;
                int currTime = coordInTime.Time;
                // Goal has been reached.
                if (currCoordinate.Equals(goal))
                    return route.MaxTimeMinutes - currTime + 1;
                // Not worth considering future paths (not enough time).
                if (currTime > route.MaxTimeMinutes)
                    continue;

                foreach (Coordinate move in MovementPossibilities(currCoordinate, route.Transporter, currTime))
                    // Optimization to stop including answers that have no chance of making it back.
                    if (move.WithinMinRange(route, currTime))
                        q.Enqueue(new CoordInTime(move, currTime + 1));
            }
            return -1;
        }

        /// <summary>
        /// Transporter acts to provide magnitude of movement.
        /// Coordinates are added together for 4 new possible coordinates.
        /// </summary>
        /// <param name="curr"></param>
        /// <param name="trans"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private IEnumerable<Coordinate> MovementPossibilities(Coordinate curr, Transporter trans, int time)
        {
            Coordinate magnitude = trans.Move(time);
            return curr + magnitude;
        }

        class CoordInTime
        {
            public CoordInTime(Coordinate coord, int time)
            {
                Coord = coord;
                Time = time;
            }

            public Coordinate Coord { get; }
            public int Time { get; }
        }
    }
}
