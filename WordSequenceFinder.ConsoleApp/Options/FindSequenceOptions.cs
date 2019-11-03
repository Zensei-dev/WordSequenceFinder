using CommandLine;

namespace WordSequenceFinder.ConsoleApp.Options
{
    public class FindSequenceOptions
    {
        [Option('d', "dictionary-location", Required = true, HelpText = "Input dictionary location.")]
        public string DictionaryLocation { get; set; }

        [Option('s', "start-word", Required = true, HelpText = "The start word of the sequence to find.")]
        public string StartWord { get; set; }

        [Option('e', "end-word", Required = true, HelpText = "The end word of the sequence to find.")]
        public string EndWord { get; set; }

        [Option('r', "result-location", Required = true, HelpText = "Sequence result location.")]
        public string ResultLocation { get; set; }
    }
}
