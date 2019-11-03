using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WordSequenceFinder.Core.FindSequence.Result;

namespace WordSequenceFinder.Infrastructure.Sequence
{
    public class SequenceResultWriter : ISequenceResultWriter
    {
        public async Task Write(SequenceResult result, string resultLocation)
        {
            var output = result.Words.Count > 0 
                ? result.Words 
                : new List<string> { "No result found!" };

            await File.WriteAllLinesAsync(resultLocation, output);
        }
    }
}
