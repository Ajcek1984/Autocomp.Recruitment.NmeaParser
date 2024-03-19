using Autocomp.Nmea.Parser.Annotations;
using Autocomp.Nmea.Parser.Resources;
using System.ComponentModel.DataAnnotations;

namespace Autocomp.Nmea.Parser.Enums
{
    public enum LongitudeTypes
    {
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.WorldDirections_East))]
        [NMEAEnumValue("E")]
        East = 0,

        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.WorldDirections_West))]
        [NMEAEnumValue("W")]
        West = 1
    }
}
