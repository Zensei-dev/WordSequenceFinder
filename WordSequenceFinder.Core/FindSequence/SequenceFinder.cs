using System;
using WordSequenceFinder.Core.Dictionary;
using WordSequenceFinder.Core.Sequence;

namespace WordSequenceFinder.Core.FindSequence
{
    public interface ISequenceFinder
    {
        SequenceResult Find(WordDictionary wordDictionary, string startWord, string endWord);
    }

    public class SequenceFinder : ISequenceFinder
    {
        public SequenceResult Find(WordDictionary wordDictionary, string startWord, string endWord)
        {
            throw new NotImplementedException();
        }
    }
}
