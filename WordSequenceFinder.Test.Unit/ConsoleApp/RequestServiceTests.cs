using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using WordSequenceFinder.ConsoleApp;
using WordSequenceFinder.ConsoleApp.Options;
using WordSequenceFinder.Core.FindSequence;
using Xunit;

namespace WordSequenceFinder.Test.Unit.ConsoleApp
{
    public class RequestServiceTests
    {
        [Fact]
        public async Task GivenFindSequenceOptions_WhenRaiseRequestForFindSequenceCommand_ThenRequestRaised()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger<RequestService>>();

            var requestService = new RequestService(mapperMock.Object, mediatorMock.Object, loggerMock.Object);
            var findSequenceOptions = new FindSequenceOptions();

            // Act
            await requestService.RaiseRequest<FindSequenceOptions, FindSequenceCommand>(findSequenceOptions);

            // Assert
            mapperMock.Verify(mapper => mapper.Map<FindSequenceCommand>(findSequenceOptions));
            mediatorMock.Verify(mediator => mediator.Send(It.IsAny<FindSequenceCommand>(), default));
        }

        // Unknown map type
        // Unknown mediation
    }
}
