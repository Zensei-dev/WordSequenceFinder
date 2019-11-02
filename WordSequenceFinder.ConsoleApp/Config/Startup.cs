using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using WordSequenceFinder.Core.Dictionary;
using WordSequenceFinder.Core.FindSequence;
using WordSequenceFinder.Core.Sequence;
using WordSequenceFinder.Infrastructure.Dictionary;
using WordSequenceFinder.Infrastructure.Sequence;

namespace WordSequenceFinder.ConsoleApp.Config
{
    public static class ConsoleStartup
    {
        public static IServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();

            services.AddLogging()
                    .AddTransient<IRequestService, RequestService>()
                    .AddAutoMapper(typeof(ConsoleStartup))
                    .AddMediatR(typeof(ConsoleStartup).GetTypeInfo().Assembly, 
                                typeof(FindSequenceRequestHandler).GetTypeInfo().Assembly)
                    .AddWordSequenceServices();

            return services.BuildServiceProvider();
        }

        public static void DisposeServices(this IServiceProvider serviceProvider)
        {
            if (serviceProvider != null & serviceProvider is IDisposable)
            {
                ((IDisposable)serviceProvider).Dispose();
            }
        }
    }

    public static class StartupExtensions
    {
        public static void AddWordSequenceServices(this IServiceCollection services)
        {
            services.AddTransient<IWordDictionaryReader, WordDictionaryReader>();
            services.AddTransient<ISequenceFinder, SequenceFinder>();
            services.AddTransient<ISequenceResultWriter, SequenceResultWriter>();
        }
    }
}
