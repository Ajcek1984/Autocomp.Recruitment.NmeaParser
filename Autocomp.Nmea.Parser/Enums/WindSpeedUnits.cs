using Autocomp.Nmea.Parser.Annotations;

namespace Autocomp.Nmea.Parser.Enums
{
    public enum WindSpeedUnits
    {
        [NMEAEnumValue("K")]
        KilometersPerHour,

        [NMEAEnumValue("M")]
        MetersPerSecond,

        [NMEAEnumValue("N")]
        Knots,

        [NMEAEnumValue("K")]
        S
    }
}
