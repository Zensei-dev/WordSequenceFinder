using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WordSequenceFinder.Core.Dictionary;
using WordSequenceFinder.Core.Sequence;

namespace WordSequenceFinder.Core.FindSequence
{
    public class FindSequenceRequestHandler : AsyncRequestHandler<FindSequenceCommand>
    {
        private readonly IWordDictionaryReader _wordDictionaryReader;
        private readonly ISequenceFinder _sequenceFinder;
        private readonly ISequenceResultWriter _sequenceResultWriter;

        public FindSequenceRequestHandler(IWordDictionaryReader wordDictionaryReader,
                                          ISequenceFinder sequenceFinder,
                                          ISequenceResultWriter sequenceResultWriter)
        {
            _wordDictionaryReader = wordDictionaryReader;
            _sequenceFinder = sequenceFinder;
            _sequenceResultWriter = sequenceResultWriter;
        }

        protected override Task Handle(FindSequenceCommand command, CancellationToken cancellationToken)
        {
            var validator = new FindSequenceCommandValidator();
            var validationResult = validator.Validate(command);

            if (validationResult.IsValid)
            {
                var wordDictionary = _wordDictionaryReader.Read(command.DictionaryLocation);

                var result = _sequenceFinder.Find(wordDictionary, command.StartWord, command.EndWord);

                _sequenceResultWriter.Write(result, command.ResultLocation);

                return Task.CompletedTask;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
