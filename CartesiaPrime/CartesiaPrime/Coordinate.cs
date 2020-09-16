using System;
using System.Collections.Generic;

namespace Cartesia_Prime
{
    public class Coordinate
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Coordinate(int _X, int _Y)
        {
            X = _X;
            Y = _Y;
        }

        public bool Equals(Coordinate other)
        {
            return (this.X == other.X && this.Y == other.Y);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            return this.Equals((Coordinate)obj);
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        /// <summary>
        /// Attempts all possible combinations of +/- X and +/- Y. Returns unique ones.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>A set of unique combinations of left and right values.</returns>
        public static ISet<Coordinate> operator +(Coordinate left, Coordinate right)
        {
            return new HashSet<Coordinate>()
            {
                new Coordinate(left.X + right.X, left.Y + right.Y),
                new Coordinate(left.X + right.X, left.Y - right.Y),
                new Coordinate(left.X - right.X, left.Y + right.Y),
                new Coordinate(left.X - right.X, left.Y - right.Y)
            };
        }

        public int Distance()
        {
            return Math.Abs(X) + Math.Abs(Y);
        }

        public bool WithinMinRange(EscapeRoute route, int currTime)
        {
            return Distance() < route.Transporter.MaxMove(route.MaxTimeMinutes - currTime + 1);
        }
    }
}