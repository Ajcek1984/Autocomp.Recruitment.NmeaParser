using Autocomp.Nmea.Parser.Annotations;
using Autocomp.Nmea.Parser.Enums;

namespace Autocomp.Nmea.Parser.Messages
{
    [NMEAMessage("MWV")]
    public class WindSpeedAndAngleNMEAMessage
    {
        [NMEATalkerDevice]
        public NMEATalkerDevices TalkerDevice { get; set; }

        [NMEAField(0)]
        public decimal WindAngle { get; set; }

        [NMEAField(1)]
        public NMEAReferenceTypes ReferenceType { get; set; }

        [NMEAField(2)]
        public decimal WindSpeed { get; set; }

        [NMEAField(3)]
        public WindSpeedUnits WindSpeedUnit { get; set; }

        [NMEABoolField(4, "A", "V")]
        public bool IsDataValid { get; set; }
    }
}
