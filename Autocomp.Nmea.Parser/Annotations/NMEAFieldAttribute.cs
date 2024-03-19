namespace Autocomp.Nmea.Parser.Annotations
{
    /// <summary>
    /// Oznacza właściwość klasy jako pole wiadomości w standardzie NMEA.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NMEAFieldAttribute : Attribute
    {
        public NMEAFieldAttribute(int order)
        {
            Order = order;
        }

        /// <summary>
        /// Numer kolejny pola w wiadomości NMEA, któremu odpowiada ta właściwość.
        /// </summary>
        public int Order { get; }
    }
}
