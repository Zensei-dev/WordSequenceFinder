using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using WordSequenceFinder.ConsoleApp.Config;
using WordSequenceFinder.ConsoleApp.Options;
using WordSequenceFinder.Core.FindSequence;

namespace WordSequenceFinder.ConsoleApp
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var services = ConsoleStartup.RegisterServices();

            var parseResult = Parser.Default.ParseArguments<FindSequenceOptions>(args);
            var requestCreator = services.GetService<IRequestService>();

            await parseResult.MapResult(
                    // Successful Parsed args
                    async (FindSequenceOptions opts) =>
                        {
                            await requestCreator.RaiseRequest<FindSequenceOptions, FindSequenceCommand>(opts);
                        },
                    // On failure, we'll let the library print errors
                    errors => Task.FromResult(-1)
                );

            services.DisposeServices();
        }
    }
}
