using Autocomp.Nmea.Parser.Annotations;
using Autocomp.Nmea.Parser.Enums;
using Autocomp.Nmea.Parser.Resources;
using System.ComponentModel.DataAnnotations;

namespace Autocomp.Nmea.Parser.Messages
{
    [NMEAMessage("MWV")]
    public class WindSpeedAndAngleNMEAMessage
    {
        [NMEATalkerDevice]
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.TalkerDevice))]
        public NMEATalkerDevices TalkerDevice { get; set; }

        [NMEAField(0)]
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.WindAngle))]
        public decimal WindAngle { get; set; }

        [NMEAField(1)]
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.ReferenceType))]
        public NMEAReferenceTypes ReferenceType { get; set; }

        [NMEAField(2)]
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.WindSpeed))]
        public decimal WindSpeed { get; set; }

        [NMEAField(3)]
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.WindSpeedUnit))]
        public WindSpeedUnits WindSpeedUnit { get; set; }

        [NMEABoolField(4, "A", "V")]
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.IsDataValid))]
        public bool IsDataValid { get; set; }
    }
}
