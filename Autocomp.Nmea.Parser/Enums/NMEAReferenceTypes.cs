using Autocomp.Nmea.Parser.Annotations;

namespace Autocomp.Nmea.Parser.Enums
{
    public enum NMEAReferenceTypes
    {
        [NMEAEnumValue("R")]
        Relative = 0,

        [NMEAEnumValue("T")]
        Theoretical = 1
    }
}
