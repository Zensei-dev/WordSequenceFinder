using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using WordSequenceFinder.Core.FindSequence.Dictionary;
using WordSequenceFinder.Core.FindSequence.Result;
using WordSequenceFinder.Core.FindSequence.Tree;

namespace WordSequenceFinder.Core.FindSequence
{
    public interface ISequenceFinder
    {
        SequenceResult Find(WordDictionary wordDictionary, string startWord, string endWord);
    }

    public class SequenceFinder : ISequenceFinder
    {
        private readonly ILogger<SequenceFinder> _logger;

        public SequenceFinder(ILogger<SequenceFinder> logger)
        {
            _logger = logger;
        }

        public SequenceResult Find(WordDictionary wordDictionary, string startWord, string endWord)
        {
            var visitedWords = new HashSet<string>();

            var rootNode = new TreeNode<string> { Value = startWord };

            var breadthFirstSearch = new Queue<TreeNode<string>>();

            breadthFirstSearch.Enqueue(rootNode);

            while (breadthFirstSearch.Count > 0)
            {
                var currentNode = breadthFirstSearch.Dequeue();

                if (currentNode.Value.Equals(endWord))
                {
                    return currentNode.ToSequenceResult();
                }

                visitedWords.Add(currentNode.Value);
                _logger.LogDebug($"Visited node {currentNode.Value}");

                var neighbours = wordDictionary.FindUnvisitedNeighbours(currentNode.Value, visitedWords);
                currentNode.AddChildren(neighbours);

                foreach (var child in currentNode.Children)
                {
                    breadthFirstSearch.Enqueue(child);
                    visitedWords.Add(child.Value);
                }
                _logger.LogDebug($"Enqueued neighbours {JsonConvert.SerializeObject(neighbours)}");
            }

            return new SequenceResult();
        }
    }
}
