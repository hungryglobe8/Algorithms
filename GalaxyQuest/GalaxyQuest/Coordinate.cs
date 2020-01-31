/* Casey Rand
 * 1/30/2020
 * CS 4400
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace GalaxyQuest
{
    /// <summary>
    /// Keeps track of an x and y coordinate.
    /// IsWithinRange determines if two coordinates are within
    /// a certain distance of each other.
    /// </summary>
    public class Coordinate
    {
        private readonly long x, y;

        /// <summary>
        /// Set the x and y coordinates of a point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Calculates if two coordinates are less than a given distance
        /// from each other.
        /// </summary>
        /// <param name="other"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public bool IsWithinRange(Coordinate other, long distance)
        {
            // Uses long arithmetic to avoid integer overflow.
            long xDiff = (this.x - other.x);
            long yDiff = (this.y - other.y);
            // x^2 + y^2 < d^2
            return xDiff * xDiff + yDiff * yDiff <= distance * distance;
        }
    }
}
