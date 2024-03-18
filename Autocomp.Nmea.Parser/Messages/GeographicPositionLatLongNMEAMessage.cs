using Autocomp.Nmea.Parser.Annotations;

namespace Autocomp.Nmea.Parser.Messages
{
    [NMEAMessage("GLL")]
    public class GeographicPositionLatLongNMEAMessage
    {
        public decimal Latitude { get; set; }

        public string LatitudeType { get; set; } = string.Empty;
    }
}
