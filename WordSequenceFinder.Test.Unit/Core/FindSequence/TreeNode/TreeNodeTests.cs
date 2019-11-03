using FluentAssertions;
using System.Collections.Generic;
using WordSequenceFinder.Core.FindSequence.Tree;
using Xunit;

namespace WordSequenceFinder.Test.Unit.Core.FindSequence.Tree
{
    public class TreeNodeTests
    {
        [Fact]
        public void GivenNewTreeNode_WhenConstruct_ThenParentNull()
        {
            var treeNode = new TreeNode<string>();

            treeNode.Parent.Should().BeNull();
        }

        [Fact]
        public void GivenTreeNode_WhenAddChildren_ThenChildrenAdded()
        {
            var treeNode = new TreeNode<string>();

            treeNode.AddChildren(new List<string> { "Test1", "Test2" });

            treeNode.Children.Count.Should().Be(2);
            treeNode.Children.Should().Contain(child => child.Value == "Test1");
            treeNode.Children.Should().Contain(child => child.Value == "Test2");
        }
    }
}
