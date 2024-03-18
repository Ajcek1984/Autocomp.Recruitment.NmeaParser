
namespace Autocomp.Nmea.Parser.Annotations
{
    public class NMEABoolFieldAttribute : NMEAFieldAttribute, ICustomNMEAFieldParser
    {
        public string TrueValue;
        public string FalseValue;

        public NMEABoolFieldAttribute(int order, string trueValue, string falseValue) : base(order)
        {
            TrueValue = trueValue;
            FalseValue = falseValue;
        }

        public object? Parse(string rawValue, Type propertyType)
        {
            if (rawValue == TrueValue) return true;
            if (rawValue == FalseValue) return false;
            throw new Exception($"Niewłaściwa wartość {rawValue}. Pole musi mieć wartość {TrueValue} lub {FalseValue}.");
        }
    }
}