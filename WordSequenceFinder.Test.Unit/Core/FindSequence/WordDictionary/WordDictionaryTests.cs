using FluentAssertions;
using System.Collections.Generic;
using WordSequenceFinder.Core.FindSequence.Dictionary;
using Xunit;

namespace WordSequenceFinder.Test.Unit.Core.FindSequence.Dictionary
{
    public class WordDictionaryTests
    {
        [Fact]
        public void GivenWordWithNoNeighbours_WhenFindNeighbours_ThenNoneFound()
        {
            var wordDictionary = new WordDictionary(new string[] { "Test", "Fail" });

            var neighbours = wordDictionary.FindUnvisitedNeighbours("Test", new HashSet<string>());

            neighbours.Count.Should().Be(0);
        }

        [Theory]
        [InlineData("test", "Test")]
        [InlineData("\\est", "Test")]
        [InlineData("1est", "Test")]
        [InlineData("CAPS","CaPS")]
        [InlineData("&and", "$and")]
        public void GivenWordWithNeighbour_WhenFindNeighbours_ThenNeighbourFound(string startWord, string neighbour)
        {
            var wordDictionary = new WordDictionary(new string[] { startWord, neighbour });

            var neighbours = wordDictionary.FindUnvisitedNeighbours(startWord, new HashSet<string>());

            neighbours.Count.Should().Be(1);
        }

        [Fact]
        public void GivenWordWithMultipleNeighbours_WhenFindNeighbours_ThenMultipleNeighboursReturned()
        {
            var wordDictionary = new WordDictionary(new string[] { "Test", "1est", "test", "\\est" });

            var neighbours = wordDictionary.FindUnvisitedNeighbours("Test", new HashSet<string>());

            neighbours.Count.Should().Be(3);
            neighbours.Should().Contain("1est");
            neighbours.Should().Contain("test");
            neighbours.Should().Contain("\\est");
        }

        [Fact]
        public void GivenNeighbourThatsAlreadyVisited_WhenFindNeighbours_ThenNoNeighbourReturned()
        {
            var wordDictionary = new WordDictionary(new string[] { "Test", "1est" });

            var neighbours = wordDictionary.FindUnvisitedNeighbours("Test", new HashSet<string> { "1est" });

            neighbours.Count.Should().Be(0);
        }
    }
}
