using System.Threading.Tasks;

namespace WordSequenceFinder.Core.Sequence
{
    public interface ISequenceResultWriter
    {
        Task Write(SequenceResult result, string resultLocation);
    }
}
