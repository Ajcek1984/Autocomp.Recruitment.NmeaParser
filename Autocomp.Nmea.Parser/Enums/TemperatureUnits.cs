using Autocomp.Nmea.Parser.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Autocomp.Nmea.Parser.Enums
{
    public enum TemperatureUnits
    {
        [Display(Name = "°C")]
        [NMEAEnumValue("C")]
        Celsius = 0
    }
}
