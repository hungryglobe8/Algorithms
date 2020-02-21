using System;
using System.Collections.Generic;

namespace AutoSink
{
    public class City
    {
        public string Name { get; private set; }
        public int Toll { get; private set; }
        private IList<Highway> highways;
        private IList<City> sources;
        // Count to keep track of road counts (sources and sinks)
        public int IncomingRoads { get; private set; }
        public int OutgoingRoads { get; private set; }
        public int PathCount { get; internal set; }
        public int TripCost { get; internal set; }

        public City(string name, int toll)
        {
            Name = name;
            Toll = toll;
            highways = new List<Highway>();
            sources = new List<City>();
        }

        /// <summary>
        /// Highways are considered the edges of the graph. They must be acyclic
        /// and travel in only one direction.
        /// </summary>
        /// <param name="otherCity"></param>
        public void AddHighway(City otherCity)
        {
            highways.Add(new Highway(this, otherCity));
            OutgoingRoads++;
            otherCity.IncomingRoads++;
            otherCity.AddSource(this);
        }

        private void AddSource(City source)
        {
            sources.Add(source);
        }

        public IList<Highway> GetHighways()
        {
            return highways;
        }

        public IList<City> GetSources()
        {
            return sources;
        }

        public IList<City> GetDestinations()
        {
            IList<City> destinations = new List<City>();
            foreach (var highway in highways)
            {
                destinations.Add(highway.Destination);
            }
            return destinations;
        }

        public bool IsSink()
        {
            return OutgoingRoads == 0;
        }
    }
}