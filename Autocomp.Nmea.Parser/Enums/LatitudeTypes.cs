using Autocomp.Nmea.Parser.Annotations;

namespace Autocomp.Nmea.Parser.Enums
{
    public enum LatitudeTypes
    {
        [NMEAEnumValue("N")]
        North = 0,

        [NMEAEnumValue("S")]
        South = 1
    }
}
