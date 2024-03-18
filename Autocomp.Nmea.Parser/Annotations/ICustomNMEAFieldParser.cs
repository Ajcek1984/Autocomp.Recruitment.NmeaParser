namespace Autocomp.Nmea.Parser.Annotations
{
    public interface ICustomNMEAFieldParser
    {
        object? Parse(string rawValue, Type propertyType);
    }
}
