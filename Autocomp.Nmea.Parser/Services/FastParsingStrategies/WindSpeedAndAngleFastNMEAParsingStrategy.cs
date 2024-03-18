using Autocomp.Nmea.Parser.Enums;
using Autocomp.Nmea.Parser.Extensions;
using Autocomp.Nmea.Parser.Messages;

namespace Autocomp.Nmea.Parser.Services.FastParsingStrategies
{
    public class WindSpeedAndAngleFastNMEAParsingStrategy : IFastNMEAParsingStrategy
    {
        public string Identifier => "MWV";

        public object Parse(Queue<string> values) => new WindSpeedAndAngleNMEAMessage
        {
            WindAngle = values.DequeueRequiredDecimal(),
            ReferenceType = values.DequeueRequiredEnum<NMEAReferenceTypes>(),
            WindSpeed = values.DequeueRequiredDecimal(),
            WindSpeedUnit = values.DequeueRequiredEnum<WindSpeedUnits>(),
            IsDataValid = values.DequeueBool("A", "V")
        };
    }
}