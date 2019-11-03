using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WordSequenceFinder.Core.Dictionary;
using WordSequenceFinder.Core.Sequence;

namespace WordSequenceFinder.Core.FindSequence
{
    public class FindSequenceRequestHandler : IRequestHandler<FindSequenceCommand, HandlerResult>
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

        public async Task<HandlerResult> Handle(FindSequenceCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await HandleCommand(command);
            }
            catch(Exception ex)
            {
                return new HandlerResult
                {
                    IsSuccessful = false,
                    ErrorList = new List<string> { "An unexpected exception has occurred" },
                    Exceptions = new List<Exception> { ex }
                };
            }
        }

        private async Task<HandlerResult> HandleCommand(FindSequenceCommand command)
        {
            var validator = new FindSequenceCommandValidator();
            var validationResult = validator.Validate(command);

            if (validationResult.IsValid)
            {
                var wordDictionary = await _wordDictionaryReader.Read(command.DictionaryLocation);

                var result = _sequenceFinder.Find(wordDictionary, command.StartWord, command.EndWord);

                await _sequenceResultWriter.Write(result, command.ResultLocation);

                return new HandlerResult()
                {
                    IsSuccessful = true,
                    Result = result
                };
            }
            else
            {
                return new HandlerResult()
                {
                    IsSuccessful = false,
                    ErrorList = validationResult.Errors.Select(err => err.ErrorMessage)
                };
            }
        }
    }
}
