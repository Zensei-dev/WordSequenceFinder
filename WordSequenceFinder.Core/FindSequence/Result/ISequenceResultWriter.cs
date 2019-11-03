using System.Threading.Tasks;

namespace WordSequenceFinder.Core.FindSequence.Result
{
    public interface ISequenceResultWriter
    {
        Task Write(SequenceResult result, string resultLocation);
    }
}
