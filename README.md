# WordSequenceFinder
Given a dictionary of words, a start word and an end word: find the shortest sequence from start word to end word

# Sample Project Notes
## Development Process
Although it could certainly be argued as overkill for this exercise, I decided to take the approach of designing the Console App in the Clean Architecture pattern, as well as using it as an opportunity to demonstrate many of the libraries / techniques / patterns I regularly use.
I designed it in a way where I could accommodate growth with the expectation that features / commands would continue to be added to the program.
The design of the project also facilitates granular unit testing with clear separation of concerns. I have added a good level of unit test coverage to cover the most important areas. Integration tests have been left out of the scope of this sample but I would also seek to implement those, especially to cover the Console App itself and the Infrastructure layer.

Some of the libraries and patterns I wanted to demonstrate in this sample include:
  - Dependency Injection and getting up and running as fast as possible for unit testing
  - MediatR & the decoupling of UI/API & Core layers
  - AutoMapper and the decoupling of UI/API & Core Model concerns
  - CommandLineParser
  - FluentValidation for Core Commands
  - xUnit, Moq & FluentAssertions for my go-to testing combination

One of the main things missing at the moment would be a configuration system, which could also potentially config different scenarios with regards to search parameters for the word dictionary.
Currently I'm hard coded to the spec of 4 length characters etc but I'd look to move that into config and make it more flexible/adaptable to different parameters.
(e.g. When reading the input dictionary, I currently prune it to only 4 letter words to ease neighbour discovery).

## Core Algorithms
There are two algorthims that deal with finding the sequence of words and a history to construct the correct sequence
  - A BFS search that can find the sequence and terminate as soon as the end word is reached (potentially preventing a lot of work crawling further word chains)
  - A neighbour discovery algorithm that compares words to find those of 1 letter difference
  - I used a custom TreeNode implementation to store the history of each BFS path and allow reverse traversal to construct the sequence from the current node, if the end word is reached
  
## Performance Notes
While my solution may not currently be the most optimal, in order to seek further improvement I'd consider the following:
  - Ideally I would have some idea about what targets I need to meet or a real life scenario that performs slowly
  - As well as algorithm analysis in core areas (BFS and Neighbour Discovery), I would use BenchmarkDotNet to start benchmarking them and compare alternative algorithms / optimisations
  - The time complexity of BFS reaches the number of words in the chain to find the result and could in the worst case be the complete length of the dictionary. In such case we could potentially compare every word to every other word to find its neighbours making neighbour discovery O(n^2)
  - If requirements allowed, pre-computing neighbours for a dictionary and storing these ready for faster lookups could provide a huge performance boost for the most costly part of the operation
  - If getting into much longer word chains and dictionaries, it looks like Bi-Directional BFS could provide a huge search space saving, I would look to implement and performance test that also if necessary
  - These are some of the intial ideas I came up with, of course I'd continue to research more as necessary
