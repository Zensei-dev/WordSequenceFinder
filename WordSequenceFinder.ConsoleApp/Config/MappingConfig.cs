using AutoMapper;
using WordSequenceFinder.ConsoleApp.Options;
using WordSequenceFinder.Core.FindSequence.Command;

namespace WordSequenceFinder.ConsoleApp.Config
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<FindSequenceOptions, FindSequenceCommand>();
        }
    }
}
