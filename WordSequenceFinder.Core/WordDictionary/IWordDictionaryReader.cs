namespace WordSequenceFinder.Core.Dictionary
{
    public interface IWordDictionaryReader
    {
        WordDictionary Read(string dictionary);
    }
}
