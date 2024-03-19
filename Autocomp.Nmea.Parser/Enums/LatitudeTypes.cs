using Autocomp.Nmea.Parser.Annotations;
using Autocomp.Nmea.Parser.Resources;
using System.ComponentModel.DataAnnotations;

namespace Autocomp.Nmea.Parser.Enums
{
    public enum LatitudeTypes
    {
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.WorldDirections_North))]
        [NMEAEnumValue("N")]
        North = 0,

        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.WorldDirections_South))]
        [NMEAEnumValue("S")]
        South = 1
    }
}
