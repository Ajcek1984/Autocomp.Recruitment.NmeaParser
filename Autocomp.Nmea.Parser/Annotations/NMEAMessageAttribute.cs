namespace Autocomp.Nmea.Parser.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class NMEAMessageAttribute : Attribute
    {
        public NMEAMessageAttribute(string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier { get; }
    }
}
