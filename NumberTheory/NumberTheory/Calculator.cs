using System;
using System.Collections.Generic;

namespace NumberTheory
{
    public static class Calculator
    {
        /// <summary>
        /// Returns the result of exponential mod x^y mod N.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="N">N > 1</param>
        /// <returns></returns>
        public static int ModEx(int x, int y, int N)
        {
            return (int)ModEx((long)x, (long)y, (long)N);
        }

        private static long ModEx(long x, long y, long N)
        {
            if (y == 0)
                return 1;

            else
            {
                long z = ModEx(x, y / 2, N);
                long result;
                if (y % 2 == 0)
                    result = (z * z) % N;
                else
                    result = ((x % N) * (z * z % N)) % N;

                while (result < 0)
                {
                    result += N;
                }
                return result;
            }
        }

        /// <summary>
        /// Computes the greatest common denominator between a and b.
        /// </summary>
        /// <param name="a">a > 0</param>
        /// <param name="b">b > 0</param>
        /// <returns></returns>
        public static int GCD(int a, int b)
        {
            if (b == 0)
                return a;
            else
                return GCD(b, a % b);
        }

        /// <summary>
        /// Computes the greatest common denominator between a and b.
        /// </summary>
        /// <param name="a">a > 0</param>
        /// <param name="b">b > 0</param>
        /// <returns></returns>
        public static int GCD(long a, long b)
        {
            if (b == 0)
                return (int) a;
            else
                return GCD(b, (a % b));
        }

        /// <summary>
        /// Computes the modular multiplicative inverse of a^-1 mod N.
        /// If the inverse does not exist, returns -1.
        /// </summary>
        /// <param name="a"> 0 < a < N </param>
        /// <param name="N"> N > a </param>
        /// <returns></returns>
        public static int ModInv(int a, int N)
        {
            Tuple<int, int, int> tuple = ExtendedEuclids(a, N);
            if (tuple.Item3 == 1)
            {
                int result = tuple.Item1 % N;
                // Make sure result is non-negative.
                while (result < 0)
                    result += N;
                return result;
            }
            else
                return -1;
        }

        /// <summary>
        /// Computes the modular multiplicative inverse of a^-1 mod N.
        /// If the inverse does not exist, returns -1.
        /// </summary>
        /// <param name="a"> 0 < a < N </param>
        /// <param name="N"> N > a </param>
        /// <returns></returns>
        public static long ModInv(long a, long N)
        {
            Tuple<long, long, long> tuple = ExtendedEuclids(a, N);
            if (tuple.Item3 == 1)
            {
                long result = tuple.Item1 % N;
                // Make sure result is non-negative.
                while (result < 0)
                    result += N;
                return result;
            }
            else
                return -1;
        }

        private static Tuple<int, int, int> ExtendedEuclids(int a, int b)
        {
            if (b == 0)
                return new Tuple<int, int, int>(1, 0, a);
            else
            {
                Tuple<int, int, int> tuple = ExtendedEuclids(b, a % b);
                int first = tuple.Item2;
                int second = tuple.Item1 - (a / b) * tuple.Item2;
                int third = tuple.Item3;
                return new Tuple<int, int, int>(first, second, third);
            }
        }

        private static Tuple<long, long, long> ExtendedEuclids(long a, long b)
        {
            if (b == 0)
                return new Tuple<long, long, long>(1, 0, a);
            else
            {
                Tuple<long, long, long> tuple = ExtendedEuclids(b, a % b);
                long first = tuple.Item2;
                long second = tuple.Item1 - (a / b) * tuple.Item2;
                long third = tuple.Item3;
                return new Tuple<long, long, long>(first, second, third);
            }
        }

        /// <summary>
        /// Checks Fermat's test using 2, 3, and 5.
        /// If p passes for those values, returns true.
        /// </summary>
        /// <param name="p"> p > 5 </param>
        /// <returns></returns>
        public static bool IsPrime(int p)
        {
            List<int> fermatNums = new List<int> { 2, 3, 5 };

            foreach (int num in fermatNums)
            {
                if (ModEx(num, p - 1, p) != 1)
                    return false;
            }
            return true;
        }

        public static int Carmichaels(int p)
        {
            int count = 0;
            for (int i = 1; i < p; i++)
            {
                if (ModEx(i, p - 1, p) == 1)
                    count++;
            }
            return count;
        }

        public static Tuple<long, int, long> Key(int p, int q)
        {
            long modulus = (long)p * (long)q;

            long phi = (long)(p - 1) * (long)(q - 1);
            int publicExp = GetLowCommonDenominator(phi);

            long privateExp = ModInv(publicExp, phi);

            return new Tuple<long, int, long>(modulus, publicExp, privateExp);
        }

        private static int GetLowCommonDenominator(long phi)
        {
            int i = 2;
            while (true)
            {
                if (GCD(i, phi) == 1)
                    return i;
                i++;
            }
        }
    }
}
