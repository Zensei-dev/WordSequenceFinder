using System.Threading.Tasks;

namespace WordSequenceFinder.Core.FindSequence.Dictionary
{
    public interface IWordDictionaryReader
    {
        Task<WordDictionary> Read(string dictionary);
    }
}
