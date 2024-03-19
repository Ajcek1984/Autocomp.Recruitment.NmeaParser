using Autocomp.Nmea.Parser.Extensions;
using Autocomp.Nmea.UnitTests.TestServices;
using Microsoft.Extensions.DependencyInjection;

namespace Autocomp.Nmea.UnitTests
{
    [TestClass]
    public class ParserTests : TestBase
    {
        [TestMethod]
        public void TestParsers() => Test<ParserTestService>();

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddNMEAParser();
        }


    }
}