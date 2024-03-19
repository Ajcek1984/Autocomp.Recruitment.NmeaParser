using Autocomp.Nmea.Parser.Annotations;

namespace Autocomp.Nmea.Parser.Enums
{
    public enum NMEATalkerDevices
    {
        [NMEAEnumValue("GP")]
        GlobalPositioningSystem = 0,

        [NMEAEnumValue("WI")]
        WeatherInstruments = 1
    }
}
