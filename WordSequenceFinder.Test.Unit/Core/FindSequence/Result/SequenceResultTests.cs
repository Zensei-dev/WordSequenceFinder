using FluentAssertions;
using System.Collections.Generic;
using WordSequenceFinder.Core.FindSequence.Result;
using Xunit;

namespace WordSequenceFinder.Test.Unit.Core.FindSequence.Result
{
    public class SequenceResultTests
    {
        [Fact]
        public void GivenConstructedWithWords_WhenQuery_ThenSequenceFoundTrue()
        {
            var sequenceResult = new SequenceResult(new List<string> { "Test" });

            sequenceResult.SequenceFound.Should().BeTrue();
            sequenceResult.Words.Count.Should().Be(1);
        }

        [Fact]
        public void GivenConstructedWithNoWords_WhenQuery_ThenSequenceFoundFalse()
        {
            var sequenceResult = new SequenceResult();

            sequenceResult.SequenceFound.Should().BeFalse();
            sequenceResult.Words.Count.Should().Be(0);
        }
    }
}
