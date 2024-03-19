namespace Autocomp.Nmea.Parser.Annotations
{
    /// <summary>
    /// Nadaj ten interfejs na atrybut NMEAFieldAttribute, aby zaimplementować niestandardowy
    /// sposób parsowania dla tego pola. Ten sposób parsowania owerrajduje domyślną metodę parsowania.
    /// </summary>
    public interface ICustomNMEAFieldParser
    {
        object? Parse(string rawValue, Type propertyType);
    }
}