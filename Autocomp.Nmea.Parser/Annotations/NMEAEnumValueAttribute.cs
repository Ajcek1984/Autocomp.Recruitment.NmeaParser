namespace Autocomp.Nmea.Parser.Annotations
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class NMEAEnumValueAttribute : Attribute
    {
        public NMEAEnumValueAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}