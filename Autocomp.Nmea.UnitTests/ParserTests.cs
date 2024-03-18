using Autocomp.Nmea.Common;
using Autocomp.Nmea.Parser.Messages;
using Autocomp.Nmea.Parser.Services;
using Autocomp.Nmea.Parser.Services.FastParsingStrategies;
using Microsoft.Extensions.DependencyInjection;

namespace Autocomp.Nmea.UnitTests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void TestParsers()
        {
            var services = new ServiceCollection();
            foreach (var type in typeof(IFastNMEAParsingStrategy).Assembly.GetTypes().Where(t => !t.IsAbstract && typeof(IFastNMEAParsingStrategy).IsAssignableFrom(t)))
                services.AddScoped(typeof(IFastNMEAParsingStrategy), type);

            services.AddScoped<NMEAParser>();

            using (var provider = services.BuildServiceProvider())
            {
                var parserWithFastStrategies = provider.GetRequiredService<NMEAParser>();
                var parserWithoutFastStrategies = new NMEAParser(Array.Empty<IFastNMEAParsingStrategy>());
                Test(parserWithFastStrategies, "$WIMWV,320,R,15.0,M,A*0B\r\n");
                Test(parserWithoutFastStrategies, "$WIMWV,320,R,15.0,M,A*0B\r\n");
            }
        }

        private void Test(NMEAParser parser, string rawMessage)
        {
            var result = parser.Parse(rawMessage) as WindSpeedAndAngleNMEAMessage;
            Assert.IsNotNull(result);
            Assert.AreEqual(320, result.WindAngle);
            Assert.AreEqual(15, result.WindSpeed);
        }
    }
}