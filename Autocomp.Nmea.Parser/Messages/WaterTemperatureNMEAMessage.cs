using Autocomp.Nmea.Parser.Annotations;
using Autocomp.Nmea.Parser.Enums;
using Autocomp.Nmea.Parser.Resources;
using System.ComponentModel.DataAnnotations;

namespace Autocomp.Nmea.Parser.Messages
{
    [NMEAMessage("MTW")]
    public class WaterTemperatureNMEAMessage
    {
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.Temperature))]
        [NMEAField(0)]
        public decimal Temperature { get; set; }

        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.TemperatureUnits))]
        [NMEAField(1)]
        public TemperatureUnits Unit { get; set; }
    }
}
