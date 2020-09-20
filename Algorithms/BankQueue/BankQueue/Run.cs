using System;
using System.Collections.Generic;

namespace BankQueue
{
    public class Run
    {
        public static void Main()
        {
            InputReader reader = new InputReader();
            int T = reader.T;
            BankPeople people = reader.People;

            int result = GetMaximumFunds(T, people);

            Console.WriteLine(result);
        }

        private static int GetMaximumFunds(int T, BankPeople people)
        {
            int result = 0;
            for (int i = T - 1; i >= 0; i--)
            {
                result += people.MostMoney(i);
            }
            return result;
        }
    }
}
