using System;
using System.Collections.Generic;
using System.Linq;

namespace NarrowArtGallery
{
    public class InputReader
    {
        public int k { get; private set; }
        public Gallery Gallery { get; private set; }

        public InputReader()
        {
            // First line contain N and k
            IList<int> result = SplitStringIntoInts(Console.ReadLine());
            int N = result[0];
            k = result[1];

            Gallery = new Gallery(N);
            // Loop over room values
            for (int i = 0; i < N; i++)
            {
                IList<int> row = SplitStringIntoInts(Console.ReadLine());
                Gallery.AddRow(row[0], row[1]);
            }

            // Last line of 0 0
            Console.ReadLine();
        }

        private IList<int> SplitStringIntoInts(string line)
        {
            IList<int> result = new List<int>();
            foreach (string word in line.Split(' ').ToList())
            {
                result.Add(int.Parse(word));
            }
            return result;
        }
    }
}
