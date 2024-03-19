using Autocomp.Nmea.Parser.Annotations;
using Autocomp.Nmea.Parser.Enums;

namespace Autocomp.Nmea.Parser.Messages
{
    [NMEAMessage("GLL")]
    public class GeographicalPositionLatLongNMEAMessage
    {
        [NMEAField(0)]
        public decimal Latitude { get; set; }

        [NMEAField(1)]
        public LatitudeTypes LatitudeType { get; set; }

        [NMEAField(2)]
        public decimal Longitude { get; set; }

        [NMEAField(3)]
        public LongitudeTypes LongitudeType { get; set; }

        [NMEAField(4)]
        public TimeSpan UTCOfPosition { get; set; }

        [NMEABoolField(5, "A", "V")]
        public bool IsValid { get; set; }

        [NMEAField(6)]
        public GeographicalPositionModes Mode { get; set; }
    }
}
