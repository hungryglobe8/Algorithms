using System;
using System.Collections.Generic;
using System.Text;

namespace GridGobble
{
    public static class Run
    {
        public static void Main()
        {
            InputReader reader = new InputReader();
            Grid grid = reader.GetGrid();

            Console.WriteLine(grid.GetMaxValue());
        }
    }
}
