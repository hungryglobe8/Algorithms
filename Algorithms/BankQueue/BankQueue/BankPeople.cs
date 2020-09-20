using System;
using System.Collections.Generic;
using System.Linq;

namespace BankQueue
{
    public class BankPeople
    {
        // Customers stores two ints, first time until a customer leaves, then amount of cash a customer has.
        public SortedList<int, IList<int>> Customers { get; private set; }
        public int NumCustomers { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        public BankPeople()
        {
            Customers = new SortedList<int, IList<int>>();
        }

        /// <summary>
        /// Adds a new person to the list of people in the bank.
        /// </summary>
        /// <param name="friend"></param>
        public void AddPerson(int time, int money)
        {
            if (Customers.ContainsKey(time))
            {
                Customers[time].Add(money);
            }
            else
            {
                Customers.Add(time, new List<int>() { money });
            }
            NumCustomers++;
        }

        /// <summary>
        /// Makes a greedy choice to find the person with the most money at a certain time.
        /// Removes person from Customers and returns their value.
        /// If no such person can be found, returns 0.
        /// </summary>
        /// <param name="friend"></param>
        public int MostMoney(int time)
        {
            int max = 0;
            int custIndex = 0;
            // Find out if there are still any customers in the store at the given time.
            IList<int> viableCustomers = CustomersStillInTheBank(time);
            foreach (int customerTime in viableCustomers)
            {
                foreach (int cash in Customers[customerTime])
                {
                    if (cash > max)
                    {
                        max = cash;
                        custIndex = customerTime;
                    }
                }
            }

            if (max != 0)
            {
                Customers[custIndex].Remove(max);
                NumCustomers--;
            }
            return max;
        }

        /// <summary>
        /// Returns a list of customers still in the store after a given time.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private IList<int> CustomersStillInTheBank(int time)
        {
            IList<int> result = new List<int>();
            foreach (int customerTime in Customers.Keys)
            {
                if (customerTime >= time)
                {
                    result.Add(customerTime);
                }
            }
            return result;
        }
    }
}