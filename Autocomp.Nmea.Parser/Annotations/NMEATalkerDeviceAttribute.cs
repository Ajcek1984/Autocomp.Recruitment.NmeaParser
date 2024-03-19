namespace Autocomp.Nmea.Parser.Annotations
{
    /// <summary>
    /// Oznacz tym atrybutem pole typu enum lub string, aby pobrać do niego
    /// nazwę urządzenia z wiadomości NMEA.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NMEATalkerDeviceAttribute : Attribute
    {
    }
}
