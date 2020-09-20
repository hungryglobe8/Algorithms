using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrAnaga
{
    public class Program
    {
        public static int Main()
        {
            string firstLine = Console.ReadLine();

            UniqueAnagramFinder MrAnaga = new UniqueAnagramFinder(firstLine);
            MrAnaga.ConvertInputTokens();
            int numUnique = MrAnaga.NumUniqueWords();
            Console.WriteLine(numUnique);
            return 0;

        }
    }
}
