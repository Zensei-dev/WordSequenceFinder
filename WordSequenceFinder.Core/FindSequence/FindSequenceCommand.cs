using MediatR;

namespace WordSequenceFinder.Core.FindSequence
{
    public class FindSequenceCommand : IRequest<HandlerResult>
    {
        public string DictionaryLocation { get; set; }
        public string StartWord { get; set; }
        public string EndWord { get; set; }
        public string ResultLocation { get; set; }
    }
}
