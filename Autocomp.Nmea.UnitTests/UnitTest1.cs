using Autocomp.Nmea.Common;
using Autocomp.Nmea.Parser.Services;

namespace Autocomp.Nmea.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var rawMessage = new NmeaMessage("$WIMWV,320,R,15.0,M,A*0B\r\n");

            var parser = new NMEAParser();
            var result = parser.Parse(rawMessage);
        }
    }
}