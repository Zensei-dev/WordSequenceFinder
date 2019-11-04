using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using WordSequenceFinder.Core.FindSequence;
using WordSequenceFinder.Core.FindSequence.Dictionary;
using Xunit;

namespace WordSequenceFinder.Test.Unit.Core.FindSequence
{
    public class SequenceFinderTests
    {
        [Fact]
        public void GivenSampleSequence_WhenFind_ThenSequenceReturned()
        {
            // Arrange
            var dictionary = new WordDictionary(new string[] { "Spin", "Spit", "Spat", "Spot", "Span" });
            var loggerMock = new Mock<ILogger<SequenceFinder>>();
            var sequenceFinder = new SequenceFinder(loggerMock.Object);

            // Act
            var result = sequenceFinder.Find(dictionary, "Spin", "Spot");

            // Assert
            result.SequenceFound.Should().BeTrue();
            result.Words.Count.Should().Be(3);
            result.Words[0].Should().Be("Spin");
            result.Words[1].Should().Be("Spit");
            result.Words[2].Should().Be("Spot");
        }

        [Fact]
        public void GivenInputWithNoSequence_WhenFind_ThenNoSequenceReturned()
        {
            // Arrange
            var dictionary = new WordDictionary(new string[] { "Spin", "Spit", "Spat", "Spot", "Span", "Test" });
            var loggerMock = new Mock<ILogger<SequenceFinder>>();
            var sequenceFinder = new SequenceFinder(loggerMock.Object);

            // Act
            var result = sequenceFinder.Find(dictionary, "Spin", "Test");

            // Assert
            result.SequenceFound.Should().BeFalse();
            result.Words.Count.Should().Be(0);
        }
    }
}
