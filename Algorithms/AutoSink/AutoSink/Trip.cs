namespace AutoSink
{
    public class Trip
    {
        public Trip(City name1, City name2)
        {
            Start = name1;
            End = name2;
        }

        public City Start { get; internal set; }
        public City End { get; internal set; }
    }
}