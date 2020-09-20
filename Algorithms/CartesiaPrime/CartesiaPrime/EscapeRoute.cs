using System.Collections.Generic;

namespace Cartesia_Prime
{
    public class EscapeRoute
    {
        public EscapeRoute(Transporter transporter, int maxTimeMinutes, Coordinate start, IList<Coordinate> droneCoordinates)
        {
            Transporter = transporter;
            MaxTimeMinutes = maxTimeMinutes;
            Start = start;
            DroneCoordinates = droneCoordinates;
        }

        public Transporter Transporter { get; private set; }
        public int MaxTimeMinutes { get; private set; }
        public Coordinate Start { get; private set; }
        public IList<Coordinate> DroneCoordinates { get; private set; }
    }
}