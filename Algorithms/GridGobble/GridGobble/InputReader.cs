using System;
using System.Collections.Generic;
using System.Text;

namespace GridGobble
{
    public class InputReader
    {
        public Grid GetGrid()
        {
            string[] tokens = Console.ReadLine().Split(' ');
            int numRows = int.Parse(tokens[0]);
            int numCols = int.Parse(tokens[1]);

            Grid grid = new Grid(numCols);
            for (int i = 0; i < numRows; i++)
            {
                IList<int> row = ReadRowFromLine();
                grid.AddRow(numRows - i - 1, row);
            }

            return grid;
        }

        private IList<int> ReadRowFromLine()
        {
            IList<int> vals = new List<int>();
            string[] line = Console.ReadLine().Split(' ');
            foreach (var val in line)
            {
                vals.Add(int.Parse(val));
            }
            return vals;
        }
    }
}
