using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoSink
{
    /// <summary>
    /// First line of input should be number of cities on the map. 
    /// Next lines contain the cities themselves, consisting of a city name and toll.
    /// Then comes number of highways, followed by which cities those highways connect.
    /// Finally read the number of trips and which cities you would like to travel between.
    /// </summary>
    public class InputReader : IReader
    {
        private IDictionary<string, City> cities = new Dictionary<string, City>();
        private IList<Trip> trips = new List<Trip>();

        /// <summary>
        /// Reads all user input to member variables.
        /// </summary>
        public void ReadInput()
        {
            // first section (1 <= n <= 2000)
            int numCities = int.Parse(Console.ReadLine());
            // read cities
            for (int i = 0; i < numCities; i++)
            {
                string line = Console.ReadLine();
                City city = ConvertStringToCity(line);
                cities.Add(city.Name, city);
            }

            // second section (0 <= h <= 10000)
            int numHighways = int.Parse(Console.ReadLine());
            // read highways
            for (int i = 0; i < numHighways; i++)
            {
                string line = Console.ReadLine();
                ConvertStringToHighway(line);
            }

            // third section (1 <= t <= 8000)
            int numTrips = int.Parse(Console.ReadLine());
            // read trips
            for (int i = 0; i < numTrips; i++)
            {
                string line = Console.ReadLine();
                Trip trip = ConvertStringToTrip(line);
                trips.Add(trip);
            }
        }

        /// <summary>
        /// Converts a string into a new City object, consisting of a name and a toll.
        /// </summary>
        /// <param name="line"> string followed by int </param>
        /// <returns> new City </returns>
        private City ConvertStringToCity(string line)
        {
            string[] tokens = line.Split(' ');
            string name = tokens[0];
            int toll = int.Parse(tokens[1]);

            return new City(name, toll);
        }
        
        /// <summary>
        /// Converts a string into a new Highway object, going from the direction
        /// of the first city to the second.
        /// </summary>
        /// <param name="line"> two strings </param>
        /// <returns> new Highway </returns>
        private void ConvertStringToHighway(string line)
        {
            string[] tokens = line.Split(' ');
            string name1 = tokens[0];
            string name2 = tokens[1];

            City city1 = cities[name1];
            City city2 = cities[name2];
            city1.AddHighway(city2);
        }

        private Trip ConvertStringToTrip(string line)
        {
            string[] tokens = line.Split(' ');
            string name1 = tokens[0];
            string name2 = tokens[1];

            City city1 = cities[name1];
            City city2 = cities[name2];
            return new Trip(city1, city2);
        }

        public IList<City> GetCities() => cities.Values.ToList();

        public IList<Trip> GetTrips() => trips;

    }
}
