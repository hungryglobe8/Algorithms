using System;

namespace Cartesia_Prime
{
    public class Transporter
    {
        private int a, b, c, d;
        /// <summary>
        /// Primary mechanism for movement inside a grid system.
        /// a and b control x movement, c and d control y movement.
        /// </summary>
        /// <param name="_a"></param>
        /// <param name="_b"></param>
        /// <param name="_c"></param>
        /// <param name="_d"></param>
        public Transporter(int _a, int _b, int _c, int _d)
        {
            a = _a;
            b = _b;
            c = _c;
            d = _d;
        }

        /// <summary>
        /// Calculates the maximum distance that can be travelled within a given time frame.
        /// </summary>
        /// <param name="timeRemaining"></param>
        /// <returns></returns>
        public int MaxMove(int timeRemaining)
        {
            return (Math.Abs(b) + Math.Abs(d)) * timeRemaining;
        }

        /// <summary>
        /// Returns the magnitude of movement that can be made in both the x and y directions.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Coordinate Move(int t)
        {
            return new Coordinate(MoveX(t), MoveY(t));
        }

        /// <summary>
        /// Returns the magnitude of movement that can be made in the x direction.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private int MoveX(int t)
        {
            return (a * t) % b;
        }

        /// <summary>
        /// Returns the magnitude of movement that can be made in the y direction.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private int MoveY(int t)
        {
            return (c * t) % d;
        }
    }
}
