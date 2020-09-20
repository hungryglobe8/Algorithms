using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrAnaga
{
    public class UniqueAnagramFinder
    {
        public int WordLength { get; private set; }
        public int WordCount { get; private set; }

        //public static int Main()
        //{
        //    UniqueAnagramFinder MrAnaga = new UniqueAnagramFinder();
        //    MrAnaga.ReadFromInputStream();
        //    int numUnique = MrAnaga.NumUniqueWords();
        //    Console.WriteLine(numUnique);
        //    return 0;
        //}
        string _firstLine;

        public UniqueAnagramFinder(string firstLine)
        {
            _firstLine = firstLine;
        }

        /// <summary>
        /// Return the number of unique anagrams from an input stream.
        /// </summary>
        /// <returns></returns>
        public int NumUniqueWords()
        {
            // Sets to be stored to.
            SortedSet<string> uniqueVals = new SortedSet<string>();
            SortedSet<string> seenVals = new SortedSet<string>();
            // Read words from stream.
            for (int i = 0; i < WordCount; i++)
            {
                string newWord = Console.ReadLine();
                // Sort the words as they are added.
                string sortedWord = SortString(newWord);
                PutWordInSet(sortedWord, seenVals, uniqueVals);
            }
            return uniqueVals.Count;
        }

        /// <summary>
        /// Returns true if a word is unique (not seen before in seenVals or uniqueVals).
        /// If false, makes sure that the word is in seenVals, not in uniqueVals.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private void PutWordInSet(string word, SortedSet<string> seenVals, SortedSet<string> uniqueVals)
        {
            if (seenVals.Contains(word))
            {
            }
            else if (uniqueVals.Contains(word))
            {
                seenVals.Add(word);
                uniqueVals.Remove(word);
            }
            else
            {
                uniqueVals.Add(word);
            }
        }

        /// <summary>
        /// Save a string with two ints separated by a space as a tuple.
        /// </summary>
        /// <param name="firstLine">string to be split</param>
        public void ConvertInputTokens()
        {
            string[] firstLineTokens = _firstLine.Split(' ');
            if (firstLineTokens.Length != 2)
                throw new Exception("Input line should have two integers seperated by a space.");

            WordCount = int.Parse(firstLineTokens[0]);
            WordLength = int.Parse(firstLineTokens[1]);
        }

        /// <summary>
        /// Reorder a string by converting it to a character array and sorting it.
        /// </summary>
        /// <param name="word">string to be sorted</param>
        /// <returns>sorted version of word</returns>
        public string SortString(string word)
        {
            char[] letters = word.ToCharArray();
            Array.Sort(letters);
            // Return a new string with sorted letters.
            return new string(letters);
        }

        private StringBuilder CreateRandomString(Random r, int wordLength, string alphabet)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < wordLength; i++)
            {
                char newLetter = alphabet[r.Next(0, alphabet.Length - 1)];
                result.Append(newLetter);
            }
            return result;
        }

        public double ConstantKTiming(int n)
        {
            int k = 5;
            Random r = new Random();
            string acceptableChars = "abcdefghijklmnopqrstuvwxyz";
            List<string> workingList = new List<string>();

            // Create random list of words for the expirement.
            for (int i = 0; i < n; i++)
            {
                StringBuilder word = CreateRandomString(r, k, acceptableChars);
                workingList.Add(word.ToString());
            }

            // Create a stopwatch
            Stopwatch sw = new Stopwatch();
            sw.Start();
            RunForOneSecond(sw, workingList);

            sw.Restart();
            RunExperiment(workingList);
            sw.Stop();
            double elapsed = Msecs(sw);
            return elapsed;
        }

        private void RunForOneSecond(Stopwatch sw, List<string> workingList)
        {
            // Keep increasing the number of repetitions until one second elapses.
            double elapsed = 0;
            int repetitions = 1;
            double DURATION = 1000;
            do
            {
                repetitions *= 2;
                sw.Restart();
                for (int i = 0; i < repetitions; i++)
                {
                    RunExperiment(workingList);
                }
                sw.Stop();
                elapsed = Msecs(sw);
            } while (elapsed < DURATION);
        }

        public double ConstantNTiming(int k)
        {
            int n = 2000;
            RandomStringGenerator generator = new RandomStringGenerator(new RandomNumberGenerator());
            IEnumerable<string> workingList = generator.CreateRandomWordList(n, k);

            // Create a stopwatch
            Stopwatch sw = new Stopwatch();
            sw.Start();
            RunForOneSecond(sw, workingList.ToList());

            sw.Restart();
            RunExperiment(workingList.ToList());
            sw.Stop();
            double elapsed = Msecs(sw);
            return elapsed;
        }

        private void RunExperiment(List<string> workingList)
        {
            // Sets to be stored to.
            SortedSet<string> uniqueVals = new SortedSet<string>();
            SortedSet<string> seenVals = new SortedSet<string>();
            // Read words.
            for (int i = 0; i < workingList.Count; i++)
            {
                // Sort the words as they are added.
                string sortedWord = SortString(workingList[i]);
                PutWordInSet(sortedWord, seenVals, uniqueVals);
            }
            int result = uniqueVals.Count;
        }

        /// <summary>
        /// Returns the number of milliseconds that have elapsed on the Stopwatch.
        /// </summary>
        public static double Msecs(Stopwatch sw)
        {
            return (((double)sw.ElapsedTicks) / Stopwatch.Frequency) * 1000;
        }
    }
}