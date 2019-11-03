using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using WordSequenceFinder.Core.FindSequence.Tree;
using Xunit;

namespace WordSequenceFinder.Test.Unit.Core.FindSequence.TreeNode
{
    public class TreeNodeExtensions
    {
        [Fact]
        public void GivenTreeNode_WhenGetSequenceResult_ThenCorrectResultReturned()
        {
            // Arrange
            var root = new TreeNode<string> { Value = "Node1" };
            root.AddChildren(new List<string> { "Node2a", "Node2b" });
            var child = root.Children.Single(child => child.Value == "Node2a");
            child.AddChildren(new List<string> { "Node3a", "Node3b", "Node3c" });

            var grandchild = child.Children.Single(child => child.Value == "Node3c");

            // Act
            var result = grandchild.ToSequenceResult();

            // Assert
            result.SequenceFound.Should().BeTrue();
            result.Words.Count.Should().Be(3);
            result.Words[0].Should().Be("Node1");
            result.Words[1].Should().Be("Node2a");
            result.Words[2].Should().Be("Node3c");
        }
    }
}
