using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using WordSequenceFinder.Core.FindSequence;
using WordSequenceFinder.Core.FindSequence.Command;
using WordSequenceFinder.Core.FindSequence.Dictionary;
using WordSequenceFinder.Core.FindSequence.Result;
using Xunit;

namespace WordSequenceFinder.Test.Unit.Core.FindSequence.Command
{
    public class FindSequenceRequestHandlerTests
    {
        private Mock<IWordDictionaryReader> _readerMock;
        private Mock<ISequenceFinder> _sequenceFinderMock;
        private Mock<ISequenceResultWriter> _writerMock;
        private Mock<ILogger<FindSequenceRequestHandler>> _loggerMock;
        private FindSequenceRequestHandler _requestHandler;
        private FindSequenceCommand _validCommand;

        public FindSequenceRequestHandlerTests()
        {
            _readerMock = new Mock<IWordDictionaryReader>();
            _sequenceFinderMock = new Mock<ISequenceFinder>();
            _writerMock = new Mock<ISequenceResultWriter>();
            _loggerMock = new Mock<ILogger<FindSequenceRequestHandler>>();

            _requestHandler = new FindSequenceRequestHandler(_readerMock.Object, _sequenceFinderMock.Object, _writerMock.Object, _loggerMock.Object);

            _validCommand = new FindSequenceCommand 
            { 
                DictionaryLocation = "TestInput", 
                StartWord = "Strt", 
                EndWord = "End.", 
                ResultLocation = "TestOutput" 
            };
        }

        [Fact]
        public async Task GivenValidCommand_WhenHandleRequest_ThenDictionaryRead()
        {
            await _requestHandler.Handle(_validCommand, default);

            _readerMock.Verify(reader => reader.Read(It.Is<string>(s => s == _validCommand.DictionaryLocation)));
        }

        [Fact]
        public async Task GivenValidCommand_WhenHandleRequest_ThenFindSequenceCalled()
        {
            await _requestHandler.Handle(_validCommand, default);

            _sequenceFinderMock.Verify(finder => finder.Find
            (
                It.IsAny<WordDictionary>(),
                It.Is<string>(s => s == _validCommand.StartWord),
                It.Is<string>(s => s == _validCommand.EndWord)
            ));
        }

        [Fact]
        public async Task GivenValidCommand_WhenHandleRequest_ThenResultWritten()
        {
            await _requestHandler.Handle(_validCommand, default);

            _writerMock.Verify(writer => writer.Write
            (
                It.IsAny<SequenceResult>(),
                It.Is<string>(s => s == _validCommand.ResultLocation)
            ));
        }

        [Fact]
        public async Task GivenValidCommand_WhenHandleRequest_ThenResultReturned()
        {
            //Arrange
            _sequenceFinderMock.Setup(seq => seq.Find
            (
                It.IsAny<WordDictionary>(),
                It.Is<string>(s => s == _validCommand.StartWord),
                It.Is<string>(s => s == _validCommand.EndWord)
            ))
                .Returns(new SequenceResult());

            // Act
            var result = await _requestHandler.Handle(_validCommand, default);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            result.Result.Should().BeOfType<SequenceResult>
                (because: $"we expect a successful {nameof(FindSequenceRequestHandler)} request to return a result of this type");
        }

        [Fact]
        public async Task GivenInvalidDictionaryLocation_WhenHandleRequest_ThenErrorMessageReturned()
        {
            // Arrange
            var command = new FindSequenceCommand
            {
                DictionaryLocation = "", // INVALID
                StartWord = "Strt",
                EndWord = "End.",
                ResultLocation = "Some/Valid/Location"
            };

            // Act
            var result = await _requestHandler.Handle(command, default);

            // Assert
            result.IsSuccessful.Should().BeFalse();
            result.ErrorList.Should().HaveCount(1);
            result.ErrorList.Should().Contain(err => err.Contains("Dictionary Location"));
        }

        [Fact]
        public async Task GivenInvalidStartWord_WhenHandleRequest_ThenErrorMessageReturned()
        {
            // Arrange
            var command = new FindSequenceCommand
            {
                DictionaryLocation = "Some/Valid/Location",
                StartWord = "", // INVALID
                EndWord = "End.",
                ResultLocation = "Some/Valid/Location"
            };

            // Act
            var result = await _requestHandler.Handle(command, default);

            // Assert
            result.IsSuccessful.Should().BeFalse();
            result.ErrorList.Should().HaveCount(1);
            result.ErrorList.Should().Contain(err => err.Contains("Start Word"));
        }

        [Fact]
        public async Task GivenInvalidEndWord_WhenHandleRequest_ThenErrorMessageReturned()
        {
            // Arrange
            var command = new FindSequenceCommand
            {
                DictionaryLocation = "Some/Valid/Location",
                StartWord = "Strt",
                EndWord = "", // INVALID
                ResultLocation = "Some/Valid/Location"
            };

            // Act
            var result = await _requestHandler.Handle(command, default);

            // Assert
            result.IsSuccessful.Should().BeFalse();
            result.ErrorList.Should().HaveCount(1);
            result.ErrorList.Should().Contain(err => err.Contains("End Word"));
        }

        [Fact]
        public async Task GivenInvalidResultLocation_WhenHandleRequest_ThenErrorMessageReturned()
        {
            // Arrange
            var command = new FindSequenceCommand
            {
                DictionaryLocation = "Some/Valid/Location", 
                StartWord = "Strt",
                EndWord = "End.",
                ResultLocation = "" // INVALID
            };

            // Act
            var result = await _requestHandler.Handle(command, default);

            // Assert
            result.IsSuccessful.Should().BeFalse();
            result.ErrorList.Should().HaveCount(1);
            result.ErrorList.Should().Contain(err => err.Contains("Result Location"));
        }
    }
}
