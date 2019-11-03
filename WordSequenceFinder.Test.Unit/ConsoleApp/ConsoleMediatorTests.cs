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
    public class ConsoleMediatorTests
    {
        [Fact]
        public async Task GivenFindSequenceOptions_WhenRaiseRequestForFindSequenceCommand_ThenRequestRaised()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            var mediatorMock = new Mock<IMediator>();
            var resultHandler = new Mock<IResultHandler>();
            var loggerMock = new Mock<ILogger<ConsoleMediator>>();

            var requestService = new ConsoleMediator(mapperMock.Object, mediatorMock.Object, resultHandler.Object, loggerMock.Object);
            var findSequenceOptions = new FindSequenceOptions();

            // Act
            await requestService.RaiseRequestAndReturnExitCodeAsync<FindSequenceOptions, FindSequenceCommand>(findSequenceOptions);

            // Assert
            mapperMock.Verify(mapper => mapper.Map<FindSequenceCommand>(findSequenceOptions));
            mediatorMock.Verify(mediator => mediator.Send(It.IsAny<FindSequenceCommand>(), default));
            resultHandler.Verify(result => result.HandleResultAndReturnExitCode(It.IsAny<HandlerResult>(), typeof(FindSequenceCommand).ToString()));
        }
    }
}
