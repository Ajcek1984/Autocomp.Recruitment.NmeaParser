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
            var testData = new Dictionary<string, object> {
                {
                    "$WIMWV,320,R,15.0,M,A*0B\r\n", new WindSpeedAndAngleNMEAMessage
                    {
                        IsDataValid = true,
                        ReferenceType = NMEAReferenceTypes.Relative,
                        WindAngle = 320m,
                        WindSpeed = 15m,
                        WindSpeedUnit = WindSpeedUnits.MetersPerSecond
                    }
                }
            };

            foreach (var pair in testData)
            {
                Test(pair.Key, pair.Value, false);
                Test(pair.Key, pair.Value, true);
            }
        }

        private void Test(string rawMessage, object assertionObject, bool disableFastStrategies)
        {
            var result = parser.Parse(rawMessage, disableFastStrategies);
            Assert.IsTrue(result.GetType() == assertionObject.GetType());
            foreach (var property in assertionObject.GetType().GetProperties())
            {
                var expectedValue = property.GetValue(assertionObject);
                var actualValue = property.GetValue(result);
                Assert.AreEqual(expectedValue, actualValue);
            }
        }

    }
}
