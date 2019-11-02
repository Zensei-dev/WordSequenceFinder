using AutoMapper;
using WordSequenceFinder.ConsoleApp.Config;
using Xunit;

namespace WordSequenceFinder.Test.Unit.ConsoleApp.Config
{
    public class MapperConfigurationTests
    {
        [Fact]
        public void GivenAMappingConfig_WhenValidateMappings_ThenMappingsValid()
        {
            var mappingConfig = new MappingConfig();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(mappingConfig);
            });

            config.AssertConfigurationIsValid();
        }
    }
}
