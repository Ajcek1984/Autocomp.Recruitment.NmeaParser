namespace Autocomp.Nmea.Parser.Annotations
{
    /// <summary>
    /// Atrybut oznaczający, że klasa jest modelem wiadomości NMEA.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class NMEAMessageAttribute : Attribute
    {
        public NMEAMessageAttribute(string identifier)
        {
            Identifier = identifier;
        }

        /// <summary>
        /// Identyfikator wiadomości
        /// </summary>
        public string Identifier { get; }
    }
}
