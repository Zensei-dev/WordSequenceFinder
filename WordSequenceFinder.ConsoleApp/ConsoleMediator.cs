using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using WordSequenceFinder.ConsoleApp.Config;
using WordSequenceFinder.Core.FindSequence;

namespace WordSequenceFinder.ConsoleApp
{
    public interface IConsoleMediator
    {
        Task<ReturnCode> RaiseRequestAndReturnExitCodeAsync<TOptions, TCommand>(TOptions opts) where TCommand : IRequest<HandlerResult>;
    }

    public class ConsoleMediator : IConsoleMediator
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IResultHandler _resultHandler;
        private readonly ILogger<ConsoleMediator> _logger;

        public ConsoleMediator(IMapper mapper, IMediator mediator, IResultHandler resultHandler, ILogger<ConsoleMediator> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _resultHandler = resultHandler;
            _logger = logger;
        }

        public async Task<ReturnCode> RaiseRequestAndReturnExitCodeAsync<TOptions, TCommand>(TOptions opts) where TCommand : IRequest<HandlerResult>
        {
            _logger.LogInformation($"Mapping {typeof(TOptions)} to {typeof(TCommand)}");
            var command = _mapper.Map<TCommand>(opts);

            _logger.LogInformation($"Raising request of type {typeof(TCommand)}");
            var result = await _mediator.Send(command);

            return _resultHandler.HandleResultAndReturnExitCode(result, typeof(TCommand).ToString());
        }
    }
}
