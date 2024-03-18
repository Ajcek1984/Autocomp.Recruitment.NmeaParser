using Autocomp.Nmea.Common;
using Autocomp.Nmea.Parser.Services;
using Autocomp.Nmea.Parser.Services.FastParsingStrategies;
using Microsoft.Extensions.DependencyInjection;

namespace Autocomp.Nmea.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var services = new ServiceCollection();
            foreach (var type in typeof(IFastNMEAParsingStrategy).Assembly.GetTypes().Where(t => !t.IsAbstract && typeof(IFastNMEAParsingStrategy).IsAssignableFrom(t)))
                services.AddScoped(typeof(IFastNMEAParsingStrategy), type);

            services.AddScoped<NMEAParser>();

            using (var provider = services.BuildServiceProvider())
            {
                var rawMessage = new NmeaMessage("$WIMWV,320,R,15.0,M,A*0B\r\n");
                var parser = provider.GetRequiredService<NMEAParser>();
                var result = parser.Parse(rawMessage);
            }
        }
    }
}