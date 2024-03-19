using Autocomp.Nmea.Parser.Annotations;
using Autocomp.Nmea.Parser.Resources;
using System.ComponentModel.DataAnnotations;

namespace Autocomp.Nmea.Parser.Enums
{
    public enum GeographicalPositionModes
    {
        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.GeographicalPositionModes_Autonomous))]
        [NMEAEnumValue("A")]
        Autonomous = 0,

        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.GeographicalPositionModes_Differential))]
        [NMEAEnumValue("D")]
        Differential = 1,

        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.GeographicalPositionModes_Estimated))]
        [NMEAEnumValue("E")]
        Estimated = 2,

        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.GeographicalPositionModes_ManualInput))]
        [NMEAEnumValue("M")]
        ManualInput = 3,

        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.GeographicalPositionModes_Simulator))]
        [NMEAEnumValue("S")]
        Simulator = 4,

        [Display(ResourceType = typeof(CommonResources), Name = nameof(CommonResources.GeographicalPositionModes_DataNotValid))]
        [NMEAEnumValue("N")]
        DataNotValid = 5
    }
}
