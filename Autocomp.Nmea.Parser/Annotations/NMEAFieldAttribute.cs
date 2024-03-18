namespace Autocomp.Nmea.Parser.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NMEAFieldAttribute : Attribute
    {
        public NMEAFieldAttribute(int order)
        {
            Order = order;
        }

        public int Order { get; }


    }
}
