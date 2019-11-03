using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WordSequenceFinder.Core.FindSequence.Dictionary;
using WordSequenceFinder.Core.FindSequence.Result;
using WordSequenceFinder.Core.Models;

namespace WordSequenceFinder.Core.FindSequence.Command
{
    public class FindSequenceRequestHandler : IRequestHandler<FindSequenceCommand, HandlerResult>
    {
        private readonly IWordDictionaryReader _wordDictionaryReader;
        private readonly ISequenceFinder _sequenceFinder;
        private readonly ISequenceResultWriter _sequenceResultWriter;
        private readonly ILogger<FindSequenceRequestHandler> _logger;

        public FindSequenceRequestHandler(IWordDictionaryReader wordDictionaryReader,
                                          ISequenceFinder sequenceFinder,
                                          ISequenceResultWriter sequenceResultWriter,
                                          ILogger<FindSequenceRequestHandler> logger)
        {
            _wordDictionaryReader = wordDictionaryReader;
            _sequenceFinder = sequenceFinder;
            _sequenceResultWriter = sequenceResultWriter;
            _logger = logger;
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
                _logger.LogInformation("Reading input dictionary");
                var wordDictionary = await _wordDictionaryReader.Read(command.DictionaryLocation);

                _logger.LogInformation("Finding word sequence");
                var result = _sequenceFinder.Find(wordDictionary, command.StartWord, command.EndWord);

                _logger.LogInformation("Writing results file");
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
