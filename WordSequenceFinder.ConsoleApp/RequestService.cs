using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WordSequenceFinder.ConsoleApp
{
    public interface IRequestService
    {
        Task RaiseRequest<TOptions, TCommand>(TOptions opts);
    }

    public class RequestService : IRequestService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<RequestService> _logger;

        public RequestService(IMapper mapper, IMediator mediator, ILogger<RequestService> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task RaiseRequest<TOptions, TCommand>(TOptions opts)
        {
            _logger.LogDebug($"Mapping {nameof(TOptions)} to {nameof(TCommand)}");
            var command = _mapper.Map<TCommand>(opts);

            _logger.LogDebug($"Raising request of type {nameof(TCommand)}");
            await _mediator.Send(command as IRequest);
        }
    }
}
