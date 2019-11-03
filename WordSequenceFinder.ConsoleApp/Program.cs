using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using WordSequenceFinder.ConsoleApp.Config;

namespace WordSequenceFinder.ConsoleApp
{
    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var services = ConsoleStartup.RegisterServices();

            var optionsHandler = services.GetService<IOptionsHandler>();

            var returnCode = await optionsHandler.ParseAndRaiseRequestAsync(args);

            services.DisposeServices();

            return (int)returnCode;
        }
    }
}
