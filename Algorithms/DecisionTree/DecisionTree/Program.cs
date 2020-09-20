using System;

namespace DecisionTree
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 10;
            int[] list = MakeNumArray(size);

            // Sorts A[lo .. hi] into ascending order
            MergeSort(list, 0, size);

            PrintResult(list);
        }

        private static void PrintResult(int[] list)
        {
            foreach (int item in list)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        private static void MergeSort(int[] list, int low, int high)
        {
            if (high - low >= 1)
            {
                int mid = (low + high) / 2;
                MergeSort(list, low, mid);
                MergeSort(list, mid + 1, high);
                Merge(list, low, mid, high);

                Console.WriteLine("Merge");
                PrintResult(list);
            }
        }


        private static void Merge(int[] list, int start, int mid, int end)
        {
            for (int i = 0; i < ; i++)
            {

            }
        }

        private static int[] MakeNumArray(int size)
        {
            int[] list = new int[size];
            Random r = new Random(5);
            for (int i = 0; i < size; i++)
            {
                list[i] = r.Next(0, size * 10);
                Console.Write(list[i] + "\t");
            }
            Console.WriteLine();
            return list;
        }
    }
}
