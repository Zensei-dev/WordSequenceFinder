using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using WordSequenceFinder.ConsoleApp;
using WordSequenceFinder.ConsoleApp.Config;
using WordSequenceFinder.Core.Models;
using Xunit;

namespace WordSequenceFinder.Test.Unit.ConsoleApp
{
    public class ConsoleResultHandlerTests
    {
        private Mock<ILogger<ConsoleResultHandler>> _loggerMock;
        private ConsoleResultHandler _resultHandler;

        public ConsoleResultHandlerTests()
        {
            _loggerMock = new Mock<ILogger<ConsoleResultHandler>>();

            _resultHandler = new ConsoleResultHandler(_loggerMock.Object);
        }

        [Fact]
        public void GivenSuccessfulResult_WhenHandleResult_ThenSuccessReturned()
        {
            var result = _resultHandler.HandleResultAndReturnExitCode(new HandlerResult { IsSuccessful = true }, "test");

            result.Should().Be(ReturnCode.Success);
        }

        [Fact]
        public void GivenFailedResult_WhenHandleResult_ThenInternalRequestErrorReturned()
        {
            var result = _resultHandler.HandleResultAndReturnExitCode(new HandlerResult { IsSuccessful = false }, "test");

            result.Should().Be(ReturnCode.InternalRequestError);
        }
    }
}
