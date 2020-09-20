namespace AutoSink
{
    public class Highway
    {
        public City Source { get; private set; }
        public City Destination { get; private set; }

        /// <summary>
        /// First city is the source of a highway. Second is the destination.
        /// </summary>
        /// <param name="city"></param>
        /// <param name="otherCity"></param>
        public Highway(City city, City otherCity)
        {
            Source = city;
            Destination = otherCity;
        }
    }
}