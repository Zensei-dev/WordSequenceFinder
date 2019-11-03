using System.IO;
using System.Threading.Tasks;
using WordSequenceFinder.Core.Dictionary;

namespace WordSequenceFinder.Infrastructure.Dictionary
{
    public class WordDictionaryReader : IWordDictionaryReader
    {
        public async Task<WordDictionary> Read(string dictionary)
        {
            var fileContents = await File.ReadAllLinesAsync(dictionary);

            return new WordDictionary(fileContents);
        }
    }
}
