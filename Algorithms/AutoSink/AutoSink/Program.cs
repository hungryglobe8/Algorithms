using System;
using System.Collections.Generic;

namespace AutoSink
{
    public class Program
    {
        public static void Main()
        {
            InputReader reader = new InputReader();
            reader.ReadInput();
            IList<City> cities = reader.GetCities();
            IList<Trip> trips = reader.GetTrips();

            Map map = new Map(cities);
            map.TopologicalSort();

            foreach (var trip in trips)
            {
                string result = map.FindCheapestPath(trip.Start, trip.End);
                Console.WriteLine(result);
            }
        }
    }
}
