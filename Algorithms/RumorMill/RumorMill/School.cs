using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RumorMill
{
    public class School
    {
        private IList<Student> _students;
        private IDictionary<int, SortedSet<string>> _visited;
        private IDictionary<Student, int> _distance;
        private IDictionary<Student, Student> _prev;
        private readonly int INFINITY = 50000;

        public School(IList<Student> students)
        {
            _students = students;
            _visited = new SortedDictionary<int, SortedSet<string>>();
            _distance = new Dictionary<Student, int>();
            _prev = new Dictionary<Student, Student>();
        }

        /// <summary>
        /// Prints the path a rumor spread to the console.
        /// </summary>
        /// <param name="rumorSpreaders"></param>
        public void GenerateReports(IList<Student> rumorSpreaders)
        {
            foreach (Student problem in rumorSpreaders)
            {
                SpreadRumor(problem);
                SortStudents();

                // Print students in order they heard the rumor.
                foreach (KeyValuePair<int, SortedSet<string>> pair in _visited)
                {
                    foreach (string name in pair.Value)
                    {
                        Console.Write(name + " ");
                    }
                }

                Console.WriteLine();
            }
        }

        private void SortStudents()
        {
            foreach (KeyValuePair<Student, int> pair in _distance)
            {
                int key = pair.Value;
                if (_visited.ContainsKey(key))
                {
                    SortedSet<string> curr = _visited[pair.Value];
                    curr.Add(pair.Key.Name);
                }
                else
                {
                    _visited.Add(key, new SortedSet<string>() { pair.Key.Name });
                }
            }
        }

        /// <summary>
        /// Uses visited and unvisited sets to determine how a rumor spread.
        /// Implements the breadth-first-search algorithm.
        /// </summary>
        /// <param name="problem"></param>
        private void SpreadRumor(Student problem)
        {
            _visited = new SortedDictionary<int, SortedSet<string>>();
            foreach (Student student in _students)
            {
                _distance[student] = INFINITY;
                _prev[student] = null;
            }

            _distance[problem] = 0;

            Queue<Student> q = new Queue<Student>();
            q.Enqueue(problem);

            while (q.Count != 0)
            {
                Student head = q.Dequeue();
                foreach (Student friend in head.Friends.Values)
                {
                    if (_distance[friend] == INFINITY)
                    {
                        q.Enqueue(friend);
                        _distance[friend] = _distance[head] + 1;
                        _prev[friend] = head;
                    }
                }
            }
        }
    }
}