using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrAnaga
{
    public class AnagramFinder2
    {
        public int FindUniqueAnagrams(int numWords, int wordLength, IEnumerable<string> words)
        {
            // Sets to be stored to.
            SortedSet<string> uniqueVals = new SortedSet<string>();
            SortedSet<string> seenVals = new SortedSet<string>();
            // Read words from stream.
            for (int i = 0; i < words.Count(); i++)
            {
                // Sort the words as they are added.
                string sortedWord = SortString(words.ToArray()[i]);
                PutWordInSet(sortedWord, seenVals, uniqueVals);
            }
            return uniqueVals.Count;
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

        /// <summary>
        /// Returns true if a word is unique (not seen before in seenVals or uniqueVals).
        /// If false, makes sure that the word is in seenVals, not in uniqueVals.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private void PutWordInSet(string word, SortedSet<string> seenVals, SortedSet<string> uniqueVals)
        {
            if (seenVals.Contains(word))
                return;

            if (uniqueVals.Contains(word))
            {
                seenVals.Add(word);
                uniqueVals.Remove(word);
            }
            else
            {
                uniqueVals.Add(word);
            }
        }

    }
}
