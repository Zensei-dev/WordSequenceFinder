using System.Threading.Tasks;

namespace WordSequenceFinder.Core.Dictionary
{
    public interface IWordDictionaryReader
    {
        Task<WordDictionary> Read(string dictionary);
    }
}
