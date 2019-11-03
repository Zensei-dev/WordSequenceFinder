using MediatR;
using WordSequenceFinder.Core.Models;

namespace WordSequenceFinder.Core.FindSequence.Command
{
    public class FindSequenceCommand : IRequest<HandlerResult>
    {
        public string DictionaryLocation { get; set; }
        public string StartWord { get; set; }
        public string EndWord { get; set; }
        public string ResultLocation { get; set; }
    }
}
