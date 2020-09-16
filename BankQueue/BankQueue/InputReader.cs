using System;
using System.Collections.Generic;
using System.Linq;

namespace BankQueue
{
    /// <summary>
    /// First line of input should be number of people in the queue and time in minutes until the bank closes.
    /// Next N lines contain the people themselves, consisting of the amount of cash in hand and how long they can wait.
    /// </summary>
    public class InputReader
    {
        public int T { get; private set; }
        public BankPeople People { get; private set; }

        /// <summary>
        /// Reads all user input to member variables.
        /// </summary>
        public InputReader()
        {
            People = new BankPeople();

            // first section (1 <= n <= 10000) (1 <= T <= 47)
            Tuple<int, int> input = ConvertStringToTuple(Console.ReadLine());
            int numCustomers = input.Item1;
            T = input.Item2;

            // read customers
            for (int i = 0; i < numCustomers; i++)
            {
                string line = Console.ReadLine();
                Tuple<int, int> customer = ConvertStringToTuple(line);
                People.AddPerson(customer.Item2, customer.Item1);
            }
        }

        /// <summary>
        /// Converts a line into a tuple of strings.
        /// </summary>
        /// <param name="line"> two strings </param>
        /// <returns> new Tuple </returns>
        private Tuple<int, int> ConvertStringToTuple(string line)
        {
            string[] tokens = line.Split(' ');
            int word1 = int.Parse(tokens[0]);
            int word2 = int.Parse(tokens[1]);

            return new Tuple<int, int>(word1, word2);
        }
    }
}