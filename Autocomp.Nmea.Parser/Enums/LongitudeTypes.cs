using Autocomp.Nmea.Parser.Annotations;

namespace Autocomp.Nmea.Parser.Enums
{
    public enum LongitudeTypes
    {
        [NMEAEnumValue("E")]
        East = 0,

        [NMEAEnumValue("W")]
        West = 1
    }
}
