using System;
using System.Collections.Generic;
using System.Text;

namespace NarrowArtGallery
{
    public static class Run
    {
        public static void Main()
        {
            InputReader r = new InputReader();
            Gallery g = r.Gallery;
            int result = g.MaxValue(0, -1, r.k);
            Console.WriteLine(result);
        }
    }
}
