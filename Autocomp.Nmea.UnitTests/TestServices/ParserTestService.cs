using Autocomp.Nmea.Parser.Enums;
using Autocomp.Nmea.Parser.Messages;
using Autocomp.Nmea.Parser.Services;

namespace Autocomp.Nmea.UnitTests.TestServices
{
    public class ParserTestService : TestServiceBase
    {
        private readonly NMEAParser parser;
        public ParserTestService(NMEAParser parser)
        {
            this.parser = parser;
        }
        public override void Test()
        {
            Test("$WIMWV,320,R,15.0,M,A*0B\r\n", new WindSpeedAndAngleNMEAMessage
            {
                IsDataValid = true,
                ReferenceType = NMEAReferenceTypes.Relative,
                WindAngle = 320m,
                WindSpeed = 15m,
                WindSpeedUnit = WindSpeedUnits.MetersPerSecond
            }, false);
            Test("$WIMWV,320,R,15.0,M,A*0B\r\n", new WindSpeedAndAngleNMEAMessage
            {
                IsDataValid = true,
                ReferenceType = NMEAReferenceTypes.Relative,
                WindAngle = 320m,
                WindSpeed = 15m,
                WindSpeedUnit = WindSpeedUnits.MetersPerSecond
            }, true);
        }

        private void Test<TResult>(string rawMessage, TResult assertionObject, bool disableFastStrategies) where TResult : class
        {
            var result = parser.Parse(rawMessage, disableFastStrategies);
            Assert.IsTrue(result is TResult);
            foreach (var property in typeof(TResult).GetProperties())
            {
                var expectedValue = property.GetValue(assertionObject);
                var actualValue = property.GetValue(result);
                Assert.AreEqual(expectedValue, actualValue);
            }
        }

    }
}
