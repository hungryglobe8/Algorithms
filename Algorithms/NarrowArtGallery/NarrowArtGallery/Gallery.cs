using System;
using System.Collections.Generic;

namespace NarrowArtGallery
{
    public class Gallery
    {
        private int[,] _rooms;
        private int _rowNum;
        private int _N;
        private Dictionary<Tuple<int, int, int>, int> _values;

        public Gallery(int N)
        {
            _rooms = new int[N, 2];
            _rowNum = 0;
            _N = N;
            _values = new Dictionary<Tuple<int, int, int>, int>();
        }

        /// <summary>
        /// Adds a new row to the gallery, increases rowNum
        /// </summary>
        /// <param name="col1"></param>
        /// <param name="col2"></param>
        public void AddRow(int col1, int col2)
        {
            _rooms[_rowNum, 0] = col1;
            _rooms[_rowNum, 1] = col2;
            _rowNum++;
        }

        /// <summary>
        /// Requires the existence of an N x 2 values array.
        /// Requires that 0 <= k <= N - r.
        /// Requires that 0 ≤ r ≤ N
        /// Requires that uncloseableRoom = -1, 0, or 1
        /// 
        /// Returns the maximum value that can be obtained from rows r through N-1
        /// when k rooms are closed, subject to this restriction: 
        /// If uncloseableRoom is 0, the room in column 0 of row r cannot be closed;
        /// If uncloseableRoom is 1, the room in column 1 of row r cannot be closed;
        /// If uncloseableRoom is -1, either room(but not both) of row i may be closed if desired.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="uncloseableRoom"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int MaxValue(int r, int uncloseableRoom, int k)
        {
            // Bottom row of gallery
            if (r == _N)
                return 0;

            int result;
            // Stored in cache
            if (_values.TryGetValue(new Tuple<int, int, int>(r, uncloseableRoom, k), out result))
                return result;

            // Save vals for future use
            int left = _rooms[r, 0] + MaxValue(r + 1, 0, k - 1);
            int right = _rooms[r, 1] + MaxValue(r + 1, 1, k - 1);

            // One entire column must be closed
            if (k == _N - r)
            {
                // Left
                if (uncloseableRoom == 0)
                    result = left;
                // Right
                if (uncloseableRoom == 1)
                    result = right;
                // Choose left or right column
                if (uncloseableRoom == -1)
                    result = Math.Max(left, right);

                _values.Add(new Tuple<int, int, int>(r, uncloseableRoom, k), result);
                return result;
            }

            // 0 < k < N - r
            else
            {
                int both = _rooms[r, 0] + _rooms[r, 1] + MaxValue(r + 1, -1, k);
                // Left
                if (uncloseableRoom == 0)
                    result = Math.Max(left, both);
                // Right
                if (uncloseableRoom == 1)
                    result = Math.Max(right, both);
                // Get the max of three paths
                if (uncloseableRoom == -1)
                    result = Math.Max(left, Math.Max(right, both));

                _values.Add(new Tuple<int, int, int>(r, uncloseableRoom, k), result);
                return result;
            }
        }
    }
}