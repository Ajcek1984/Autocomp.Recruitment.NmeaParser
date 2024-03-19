using Autocomp.Nmea.Parser.Services;
using Autocomp.Nmea.Parser.Services.FastParsingStrategies;
using Microsoft.Extensions.DependencyInjection;

namespace Autocomp.Nmea.Parser.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddNMEAParser(this IServiceCollection services)
        {
            services.AddScoped<NMEAParser>();

            foreach (var type in typeof(IFastNMEAParsingStrategy).Assembly.GetTypes().Where(t => !t.IsAbstract && typeof(IFastNMEAParsingStrategy).IsAssignableFrom(t)))
                services.AddScoped(typeof(IFastNMEAParsingStrategy), type);

            return services;
        }
    }
}
