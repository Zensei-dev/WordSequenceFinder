using System.IO;
using System.Threading.Tasks;
using WordSequenceFinder.Core.Sequence;

namespace WordSequenceFinder.Infrastructure.Sequence
{
    public class SequenceResultWriter : ISequenceResultWriter
    {
        public async Task Write(SequenceResult result, string resultLocation)
        {
            await File.WriteAllLinesAsync(resultLocation, result.GetResult());
        }
    }
}
