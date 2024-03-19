using Autocomp.Nmea.Common;
using Autocomp.Nmea.Parser.Enums;
using Autocomp.Nmea.Parser.Extensions;
using Autocomp.Nmea.Parser.Messages;
using Autocomp.Nmea.Parser.Services;

namespace Autocomp.Nmea.UnitTests.TestServices
{
    public class ParserTestService : TestServiceBase
    {
        private readonly NMEAParserService parser;
        public ParserTestService(NMEAParserService parser)
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
                        WindSpeedUnit = WindSpeedUnits.MetersPerSecond,
                        TalkerDevice = NMEATalkerDevices.WeatherInstruments
                    }
                },
                {
                    "$GPGLL,5057.970,N,00146.110,E,142451,A*27\r\n", new GeographicalPositionLatLongNMEAMessage
                    {
                        Latitude = 5057.970m,
                        LatitudeType = LatitudeTypes.North,
                        Longitude = 00146.110m,
                        LongitudeType = LongitudeTypes.East,
                        UTCOfPosition = new TimeSpan(14,24,51),
                        IsValid = true,
                        TalkerDevice = NMEATalkerDevices.GlobalPositioningSystem
                    }
                },
                {
                    "$GPGLL,3953.88008971,N,10506.75318910,W,034138.00,A,D*7A\r\n", new GeographicalPositionLatLongNMEAMessage
                    {
                        Latitude = 3953.88008971m,
                        LatitudeType = LatitudeTypes.North,
                        Longitude = 10506.75318910m,
                        LongitudeType = LongitudeTypes.West,
                        UTCOfPosition = new TimeSpan(03,41,38),
                        IsValid = true,
                        Mode = GeographicalPositionModes.Differential,
                        TalkerDevice = NMEATalkerDevices.GlobalPositioningSystem
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
            var message = new NmeaMessage(rawMessage);
            
            var calculatedCrc = message.CalculateCrc();
            var embeddedCrc = message.GetCrc();
            Assert.AreEqual(calculatedCrc, embeddedCrc);

            var result = parser.Parse(message, disableFastStrategies);
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
