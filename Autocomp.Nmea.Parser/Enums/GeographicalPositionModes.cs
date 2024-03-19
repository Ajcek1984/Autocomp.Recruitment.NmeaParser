using Autocomp.Nmea.Parser.Annotations;

namespace Autocomp.Nmea.Parser.Enums
{
    public enum GeographicalPositionModes
    {
        [NMEAEnumValue("A")]
        Autonomous = 0,

        [NMEAEnumValue("D")]
        Differential = 1,

        [NMEAEnumValue("E")]
        Estimated = 2,

        [NMEAEnumValue("M")]
        ManualInput = 3,

        [NMEAEnumValue("S")]
        Simulator = 4,

        [NMEAEnumValue("N")]
        DataNotValid = 5
    }
}
