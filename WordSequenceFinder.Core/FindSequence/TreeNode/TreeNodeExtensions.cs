using System.Collections.Generic;
using WordSequenceFinder.Core.FindSequence.Result;

namespace WordSequenceFinder.Core.FindSequence.Tree
{
    public static class TreeNodeExtensions
    {
        public static SequenceResult ToSequenceResult(this TreeNode<string> treeNode)
        {
            // Starting from bottom
            TreeNode<string> currentNode = treeNode;
            var sequenceResult = new List<string>();

            while (currentNode.Parent != null)
            {
                sequenceResult.Add(currentNode.Value);
                currentNode = currentNode.Parent;
            }

            sequenceResult.Add(currentNode.Value); // Root

            sequenceResult.Reverse();

            return new SequenceResult(sequenceResult);
        }
    }
}
