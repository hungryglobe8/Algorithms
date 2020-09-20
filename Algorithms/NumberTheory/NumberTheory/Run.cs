using System;
using System.Collections.Generic;
using System.Text;

namespace NumberTheory
{
    public static class Run
    {
        public static void Main()
        {
            List<string> output = new List<string>();
            // Consider timer?
            while (true)
            {
                string line = Console.ReadLine();
                if (line == null)
                    break;
                output.Add(FindAlgorithm(line.Split(' ')));
            }

            foreach (string result in output)
            {
                Console.WriteLine(result);
            }
        }

        private static string FindAlgorithm(string[] line)
        {
            string key = line[0];
            int a, b, x, y, N, p, q;
            switch (key)
            {
                case "gcd":
                    a = int.Parse(line[1]);
                    b = int.Parse(line[2]);
                    return Calculator.GCD(a, b).ToString();
                case "exp":
                    x = int.Parse(line[1]);
                    y = int.Parse(line[2]);
                    N = int.Parse(line[3]);
                    return Calculator.ModEx(x, y, N).ToString();
                case "inverse":
                    a = int.Parse(line[1]);
                    N = int.Parse(line[2]);
                    int result = Calculator.ModInv(a, N);
                    if (result == -1)
                        return "none";
                    else
                        return result.ToString();
                case "isprime":
                    p = int.Parse(line[1]);
                    if (Calculator.IsPrime(p))
                        return "yes";
                    else
                        return "no";
                case "key":
                    p = int.Parse(line[1]);
                    q = int.Parse(line[2]);
                    Tuple<long, int, long> tuple = Calculator.Key(p, q);
                    return $"{tuple.Item1} {tuple.Item2} {tuple.Item3}";
                default:
                    return "";
            }
        }
    }
}
