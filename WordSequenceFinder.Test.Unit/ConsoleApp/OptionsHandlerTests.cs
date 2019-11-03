using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using WordSequenceFinder.ConsoleApp;
using WordSequenceFinder.ConsoleApp.Config;
using WordSequenceFinder.ConsoleApp.Options;
using WordSequenceFinder.Core.FindSequence.Command;
using Xunit;

namespace WordSequenceFinder.Test.Unit.ConsoleApp
{
    public class OptionsHandlerTests
    {
        private Mock<IConsoleMediator> _requestMediatorMock;
        private Mock<ILogger<OptionsHandler>> _loggerMock;
        private OptionsHandler _optionsHandler;

        public OptionsHandlerTests()
        {
            _requestMediatorMock = new Mock<IConsoleMediator>();
            _loggerMock = new Mock<ILogger<OptionsHandler>>();
            _optionsHandler = new OptionsHandler(_requestMediatorMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GivenFindSequenceOptions_WhenParseOptions_ThenFindSequenceCommandRequestRaised()
        {
            var result = await _optionsHandler.ParseAndRaiseRequestAsync(new string[] { "find-sequence", "-d", "input", "-s", "start", "-e", "end", "-r", "result" });

            result.Should().Be(ReturnCode.Success);

            _requestMediatorMock.Verify(mediator => 
                                            mediator.RaiseRequestAndReturnExitCodeAsync<FindSequenceOptions, FindSequenceCommand>(
                                                It.IsAny<FindSequenceOptions>()
                                            )
                                        );
        } 

        [Fact]
        public async Task GivenInvalidArguments_WhenParseOptions_ThenErrorCodeReturned()
        {
            var result = await _optionsHandler.ParseAndRaiseRequestAsync(new string[] { "unknown-verb" });

            result.Should().Be(ReturnCode.InvalidArguments);

            _requestMediatorMock.VerifyNoOtherCalls();
        }
    }
}
