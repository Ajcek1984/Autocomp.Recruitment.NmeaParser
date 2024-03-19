using Autocomp.Nmea.Parser.Annotations;
using Autocomp.Nmea.Parser.Resources;
using System.ComponentModel.DataAnnotations;

namespace Autocomp.Nmea.Parser.Enums
{
    public enum NMEAReferenceTypes
    {
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.NMEAReferenceTypes_Relative))]
        [NMEAEnumValue("R")]
        Relative = 0,

        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.NMEAReferenceTypes_Theoretical))]
        [NMEAEnumValue("T")]
        Theoretical = 1
    }
}
