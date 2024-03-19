using Autocomp.Nmea.Parser.Annotations;
using Autocomp.Nmea.Parser.Enums;
using Autocomp.Nmea.Parser.Resources;
using System.ComponentModel.DataAnnotations;

namespace Autocomp.Nmea.Parser.Messages
{
    [NMEAMessage("GLL")]
    public class GeographicalPositionLatLongNMEAMessage
    {
        [NMEATalkerDevice]
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.TalkerDevice))]
        public NMEATalkerDevices TalkerDevice { get;set; }

        [NMEAField(0)]
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.Latitude))]
        public decimal Latitude { get; set; }

        [NMEAField(1)]
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.LatitudeType))]
        public LatitudeTypes LatitudeType { get; set; }

        [NMEAField(2)]
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.Longitude))]
        public decimal Longitude { get; set; }

        [NMEAField(3)]
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.LongitudeType))]
        public LongitudeTypes LongitudeType { get; set; }

        [NMEAField(4)]
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.UTCOfPosition))]
        public TimeSpan UTCOfPosition { get; set; }

        [NMEABoolField(5, "A", "V")]
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.IsDataValid))]
        public bool IsValid { get; set; }

        [NMEAField(6)]
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.Mode))]
        public GeographicalPositionModes Mode { get; set; }
    }
}
