using System.IO;
using WordSequenceFinder.Core.Sequence;

namespace WordSequenceFinder.Infrastructure.Sequence
{
    public class SequenceResultWriter : ISequenceResultWriter
    {
        public void Write(SequenceResult result, string resultLocation)
        {
            File.WriteAllLines(resultLocation, result.GetResult());
        }
    }
}
