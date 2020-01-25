using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrAnaga
{

    public class RandomStringGenerator
    {
        private IRandomNumberGenerator _numberGenerator;
        readonly string _alphabet = "abcdefghijklmnopqrstuvwxyz";

        public RandomStringGenerator(IRandomNumberGenerator numberGenerator)
        {
            _numberGenerator = numberGenerator;
        }

        public string CreateRandomString(int wordLength)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < wordLength; i++)
            {
                char newLetter = _alphabet[_numberGenerator.Next(0, _alphabet.Length - 1)];
                result.Append(newLetter);
            }
            return result.ToString();
        }
    }

    public static class RandomStringGeneratorExtensions
    {
        public static IEnumerable<string> CreateRandomWordList(this RandomStringGenerator generator, int numWords, int wordLength)
        {
            List<string> workingList = new List<string>();

            // Create random list of words for the expirement.
            for (int i = 0; i < numWords; i++)
            {
                string word = generator.CreateRandomString(wordLength);
                workingList.Add(word);
            }

            return workingList;
        }
    }


}
