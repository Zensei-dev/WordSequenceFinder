using System.IO;
using WordSequenceFinder.Core.Dictionary;

namespace WordSequenceFinder.Infrastructure.Dictionary
{
    public class WordDictionaryReader : IWordDictionaryReader
    {
        public WordDictionary Read(string dictionary)
        {
            var fileContents = File.ReadAllLines(dictionary);

            return new WordDictionary(fileContents);
        }
    }
}
