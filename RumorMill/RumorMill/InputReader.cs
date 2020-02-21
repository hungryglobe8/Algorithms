using System;
using System.Collections.Generic;
using System.Linq;

namespace RumorMill
{
    /// <summary>
    /// First line of input should be number of cities on the map. 
    /// Next lines contain the cities themselves, consisting of a city name and toll.
    /// Then comes number of highways, followed by which cities those highways connect.
    /// Finally read the number of trips and which cities you would like to travel between.
    /// </summary>
    public class InputReader : IReader
    {
        private IDictionary<string, Student> students = new Dictionary<string, Student>();
        private IList<Student> rumors = new List<Student>();

        /// <summary>
        /// Reads all user input to member variables.
        /// </summary>
        public void ReadInput()
        {
            // first section (1 <= n <= 2000)
            int numStudents = int.Parse(Console.ReadLine());
            // read student
            for (int i = 0; i < numStudents; i++)
            {
                string name = Console.ReadLine();
                Student student = new Student(name);
                students.Add(name, student);
            }

            // second section (0 <= f <= 10000)
            int numFriendPairs = int.Parse(Console.ReadLine());
            // read friends
            for (int i = 0; i < numFriendPairs; i++)
            {
                string line = Console.ReadLine();
                ConvertStringToFriends(line);
            }

            // third section (1 <= r <= 2000)
            int numReports = int.Parse(Console.ReadLine());
            // read trips
            for (int i = 0; i < numReports; i++)
            {
                string name = Console.ReadLine();
                Student student = students[name];
                rumors.Add(student);
            }
        }

        /// <summary>
        /// Converts a string into a new Highway object, going from the direction
        /// of the first city to the second.
        /// </summary>
        /// <param name="line"> two strings </param>
        /// <returns> new Highway </returns>
        private void ConvertStringToFriends(string line)
        {
            string[] tokens = line.Split(' ');
            string name1 = tokens[0];
            string name2 = tokens[1];

            Student student1 = students[name1];
            Student student2 = students[name2];
            student1.MakeConnection(student2);
        }

        public IList<Student> GetStudents()
        {
            return students.Values.ToList();
        }

        public IList<Student> GetRumors()
        {
            return rumors;
        }
    }
}