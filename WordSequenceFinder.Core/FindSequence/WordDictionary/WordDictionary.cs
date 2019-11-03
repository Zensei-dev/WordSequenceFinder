using System.Collections.Generic;
using System.Linq;

namespace WordSequenceFinder.Core.FindSequence.Dictionary
{
    public class WordDictionary
    {
        public IEnumerable<string> Words { get; set; }

        public WordDictionary(string[] wordArray)
        {
            Words = wordArray.Where(word => word.Length == 4);
        }

        public List<string> FindUnvisitedNeighbours(string currentWord, HashSet<string> visitedWords)
        {
            var neighbours = new List<string>();

            foreach (var word in Words)
            {
                if (!visitedWords.Contains(word))
                {
                    int allowedCharDifference = 1;
                    int charDifferenceCount = 0;

                    int i = 0;

                    while (charDifferenceCount <= allowedCharDifference
                           && i < word.Length)
                    {
                        if (word[i] != currentWord[i])
                            charDifferenceCount++;

                        i++;
                    }

                    if (charDifferenceCount == allowedCharDifference)
                        neighbours.Add(word);
                }
            }

            return neighbours;
        }
    }
}
