using Autocomp.Nmea.Parser.Annotations;
using Autocomp.Nmea.Parser.Resources;
using System.ComponentModel.DataAnnotations;

namespace Autocomp.Nmea.Parser.Enums
{
    public enum WindSpeedUnits
    {
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.WindSpeedUnits_KMPerHour))]
        [NMEAEnumValue("K")]
        KilometersPerHour,

        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.WindSpeedUnits_MetersPerSecond))]
        [NMEAEnumValue("M")]
        MetersPerSecond,

        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.WindSpeedUnits_Knots))]
        [NMEAEnumValue("N")]
        Knots,

        [NMEAEnumValue("K")]
        S
    }
}
