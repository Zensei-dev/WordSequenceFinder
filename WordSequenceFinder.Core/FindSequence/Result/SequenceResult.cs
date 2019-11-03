using System.Collections.Generic;

namespace WordSequenceFinder.Core.FindSequence.Result
{
    public class SequenceResult
    {
        public bool SequenceFound { get; } = false;
        public List<string> Words { get; } = new List<string>();

        public SequenceResult() { }

        public SequenceResult(List<string> words)
        {
            if (words != null && words.Count > 0)
            {
                Words = words;
                SequenceFound = true;
            }
            else
            {
                SequenceFound = false;
            }
        }
    }
}
