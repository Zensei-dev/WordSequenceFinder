using CommandLine;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WordSequenceFinder.ConsoleApp.Config;
using WordSequenceFinder.ConsoleApp.Options;
using WordSequenceFinder.Core.FindSequence.Command;

namespace WordSequenceFinder.ConsoleApp
{
    public interface IOptionsHandler
    {
        Task<ReturnCode> ParseAndRaiseRequestAsync(string[] args);
    }

    public class OptionsHandler : IOptionsHandler
    {
        private readonly IConsoleMediator _consoleMediator;
        private readonly ILogger<OptionsHandler> _logger;

        public OptionsHandler(IConsoleMediator consoleMediator, ILogger<OptionsHandler> logger)
        {
            _consoleMediator = consoleMediator;
            _logger = logger;
        }

        public async Task<ReturnCode> ParseAndRaiseRequestAsync(string[] args)
        {
            try
            {
                _logger.LogInformation("Parsing command line arguments");
                var parseResult = Parser.Default.ParseArguments<FindSequenceOptions>(args);

                return await parseResult.MapResult
                (
                    // Successful parse, raise calls for known verb types
                    async (FindSequenceOptions opts) =>
                        { return await _consoleMediator.RaiseRequestAndReturnExitCodeAsync<FindSequenceOptions, FindSequenceCommand>(opts); },

                    // On failed parse, we'll let the library print errors
                    errors => Task.FromResult(ReturnCode.InvalidArguments)
                );
            }
            catch (Exception ex)
            {
                _logger.LogError("An unknown error has occured", ex);
                _logger.LogError(ex.Message);
                return ReturnCode.UnknownError;
            }
        }
    }
}
