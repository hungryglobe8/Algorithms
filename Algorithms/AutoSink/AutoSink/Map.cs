using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoSink
{
    public class Map
    {
        private IList<City> unsortedCities, sortedCities;
        private IDictionary<int, City> previsit, postvisit;
        private int clock;
        private readonly int MAX_TRAVEL_DISTANCE = 1000000000;

        public Map(IList<City> cities)
        {
            unsortedCities = cities;
        }

        public void TopologicalSort()
        {
            sortedCities = new List<City>();
            previsit = new Dictionary<int, City>();
            postvisit = new Dictionary<int, City>();
            clock = 1;
            IDictionary<City, bool> visited = new Dictionary<City, bool>();

            // Make sure visited is filled.
            foreach (var city in unsortedCities)
            {
                visited.Add(city, false);
            }

            foreach (var city in unsortedCities)
            {
                if (!visited[city])
                    Explore(visited, city);
            }

            var list = postvisit.Keys.ToList();
            list.Sort();
            list.Reverse();
            foreach (var key in list)
            {
                sortedCities.Add(postvisit[key]);
            }
        }

        public IList<City> GetSortedCities()
        {
            return sortedCities;
        }

        /// <summary>
        /// Attempt to explore a city and all cities connected to it.
        /// </summary>
        /// <param name="source"></param>
        private void Explore(IDictionary<City, bool> visited, City source)
        {
            visited[source] = true;
            previsit[clock++] = source;
            foreach (var destination in source.GetDestinations())
            {
                if (!visited[destination])
                    Explore(visited, destination);
            }
            postvisit[clock++] = source;
        }

        public IList<City> FindSources()
        {
            IList<City> sources = new List<City>();
            foreach (var city in unsortedCities)
            {
                if (city.IncomingRoads == 0)
                    sources.Add(city);
            }
            return sources;
        }

        public string FindCheapestPath(City startCity, City finishCity)
        {
            int numPaths = FindNumPaths(startCity, finishCity);
            if (numPaths == 0)
            {
                if (startCity == finishCity)
                    return "0";

                else 
                    return "NO";
            }

            IList<City> reverse = sortedCities.Reverse<City>().ToList<City>();
            int start = reverse.IndexOf(finishCity);
            int finish = reverse.IndexOf(startCity);
            IList<City> potentialPaths = reverse.ToList<City>().GetRange(start, finish - start + 1);
            // Set up relevant trip costs.
            foreach (var city in sortedCities)
            {
                city.TripCost = MAX_TRAVEL_DISTANCE;
            }
            // Destination city should have travel cost of 0.
            potentialPaths[0].TripCost = 0;

            foreach (var city in potentialPaths)
            {
                if (city == startCity)
                    break;

                foreach (var source in city.GetSources())
                {
                    int travelCost = city.Toll + city.TripCost;
                    // If travel is cheaper one way, replace the value.
                    if (travelCost < source.TripCost)
                        source.TripCost = travelCost;
                }
            }

            return startCity.TripCost.ToString();
        }

        public int FindNumPaths(City startCity, City finishCity)
        {
            int start = sortedCities.IndexOf(startCity);
            int finish = sortedCities.IndexOf(finishCity);

            if (start >= finish || startCity.IsSink())
                return 0;

            else
            {
                IList<City> potentialPaths = sortedCities.ToList<City>().GetRange(start, finish - start + 1);

                // Set up relevant path counts.
                foreach (var city in potentialPaths)
                {
                    city.PathCount = 0;
                }
                sortedCities[start].PathCount = 1;

                for (int i = start + 1; i < finish + 1; i++)
                {
                    City city = sortedCities[i];
                    foreach (var source in city.GetSources())
                    {
                        if (potentialPaths.Contains(source))
                        {
                            city.PathCount += source.PathCount;
                        }
                    }
                }

                return sortedCities[finish].PathCount;
            }
        }
    }
}
