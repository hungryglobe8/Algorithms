using System;
using System.Collections.Generic;
using System.Linq;

namespace GridGobble
{
    public class Grid
    {
        public int NumRows { get; set; }
        public int NumCols { get; set; }
        private IDictionary<int, int[]> values;
        private IDictionary<Tuple<int, int>, int> maxValues;

        public Grid(int numCols)
        {
            values = new Dictionary<int, int[]>();
            maxValues = new Dictionary<Tuple<int, int>, int>();
            NumRows = 0;
            NumCols = numCols;
        }

        public void AddRow(IList<int> row)
        {
            values.Add(NumRows, row.ToArray());
            NumRows++;
        }

        public void AddRow(int index, IList<int> row)
        {
            values.Add(index, row.ToArray());
            NumRows++;
        }

        public int[] GetRow(int i)
        {
            return values[i];
        }

        public int GetMaxValue()
        {
            IList<int> results = new List<int>();
            for (int i = 0; i < NumCols; i++)
            {
                results.Add(GetMaxValue(0, i));
            }
            return results.Max();
        }

        private int GetMaxValue(int row, int col)
        {
            int curr = values[row][col];
            // On the last row
            if (row + 1 == NumRows)
                return curr;

            int result;
            // Stored in cache
            if (maxValues.TryGetValue(new Tuple<int, int>(row, col), out result))
                return result;

            // Diagonal left
            int left;
            if (col == 0)
                left = GetMaxValue(row + 1, NumCols - 1) - (2 * values[row + 1][NumCols - 1]);
            else
                left = GetMaxValue(row + 1, col - 1) - (2 * values[row + 1][col - 1]);

            // Middle
            int middle = GetMaxValue(row + 1, col);

            // Diagonal right
            int right;
            if (col == NumCols - 1)
                right = GetMaxValue(row + 1, 0) - (2 * values[row + 1][0]);
            else
                right = GetMaxValue(row + 1, col + 1) - (2 * values[row + 1][col + 1]);

            result = Math.Max(left + curr, Math.Max(middle + curr, right + curr));
            maxValues.Add(new Tuple<int, int>(row, col), result);
            return result;
        }
    }
}
