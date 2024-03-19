using Autocomp.Nmea.Parser.Annotations;
using Autocomp.Nmea.Parser.Resources;
using System.ComponentModel.DataAnnotations;

namespace Autocomp.Nmea.Parser.Enums
{
    public enum NMEATalkerDevices
    {
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.NMEATalkerDevices_GlobalPositioningSystem))]
        [NMEAEnumValue("GP")]
        GlobalPositioningSystem = 0,

        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.NMEATalkerDevices_WeatherInstruments))]
        [NMEAEnumValue("WI")]
        WeatherInstruments = 1
    }
}
