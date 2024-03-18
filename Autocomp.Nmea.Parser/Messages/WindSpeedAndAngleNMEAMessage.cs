using Autocomp.Nmea.Parser.Annotations;
using Autocomp.Nmea.Parser.Enums;

namespace Autocomp.Nmea.Parser.Messages
{
    [NMEAMessage("MWV")]
    public class WindSpeedAndAngleNMEAMessage
    {
        [NMEAField(0)]
        public decimal WindAngle { get; set; }

        [NMEAField(1)]
        public NMEAReferenceTypes ReferenceType { get; set; }

        [NMEAField(2)]
        public decimal WindSpeed { get; set; }

        [NMEAField(3)]
        public WindSpeedUnits WindSpeedUnit { get; set; }

        [NMEAField(4)]
        public bool IsDataValid { get; set; }
    }
}
