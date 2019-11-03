using System.Collections.Generic;
using System.Linq;

namespace WordSequenceFinder.Core.FindSequence.Tree
{
    public class TreeNode<T>
    {
        public TreeNode<T> Parent { get; set; } = null;
        public T Value { get; set; }
        public List<TreeNode<T>> Children { get; set; } = new List<TreeNode<T>>();

        public void AddChildren(List<T> values)
        {
            Children.AddRange
            (
                values.Select(value => new TreeNode<T> { Parent = this, Value = value })
            );
        }
    }
}
