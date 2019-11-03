using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using WordSequenceFinder.ConsoleApp.Config;
using WordSequenceFinder.Core.Models;

namespace WordSequenceFinder.ConsoleApp
{
    public interface IResultHandler
    {
        ReturnCode HandleResultAndReturnExitCode(HandlerResult handlerResult, string requestName);
    }

    public class ConsoleResultHandler : IResultHandler
    {
        private readonly ILogger<ConsoleResultHandler> _logger;

        public ConsoleResultHandler(ILogger<ConsoleResultHandler> logger)
        {
            _logger = logger;
        }

        public ReturnCode HandleResultAndReturnExitCode(HandlerResult handlerResult, string requestName)
        {
            if (handlerResult.IsSuccessful)
            {
                return HandleSuccess(handlerResult, requestName);
            }
            else
            {
                return HandleFailure(handlerResult, requestName);
            }
        }

        private ReturnCode HandleSuccess(HandlerResult handlerResult, string requestName)
        {
            _logger.LogInformation($"{requestName} successfully executed");

            if (handlerResult.Result != null)
            {
                _logger.LogInformation("RESULT:");
                _logger.LogInformation(JsonConvert.SerializeObject(handlerResult.Result));
            }

            return ReturnCode.Success;
        }

        private ReturnCode HandleFailure(HandlerResult handlerResult, string requestName)
        {
            _logger.LogError($"{requestName} encountered internal error(s)");

            if (handlerResult.ErrorList.Count() > 0)
            {
                _logger.LogError("ERROR LIST:");
                _logger.LogError(JsonConvert.SerializeObject(handlerResult.ErrorList));
            }

            if (handlerResult.Exceptions.Count() > 0)
            {
                _logger.LogError("EXCEPTIONS:");
                _logger.LogError(JsonConvert.SerializeObject(handlerResult.Exceptions));
            }

            return ReturnCode.InternalRequestError;
        }
    }
}
