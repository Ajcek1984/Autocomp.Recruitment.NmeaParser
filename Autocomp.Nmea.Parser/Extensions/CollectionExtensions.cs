using Autocomp.Nmea.Parser.Services;
using System.Globalization;

namespace Autocomp.Nmea.Parser.Extensions
{
    public static class CollectionExtensions
    {
        public static decimal? DequeueDecimal(this Queue<string> queue)
        {
            if (!queue.Any())
                return null;

            var rawValue = queue.Dequeue();
            if (decimal.TryParse(rawValue, CultureInfo.InvariantCulture, out var value))
                return value;

            return null;
        }

        public static decimal DequeueRequiredDecimal(this Queue<string> queue) => DequeueDecimal(queue) ??
            throw new Exception("Oczekiwano wartości decimal.");

        public static TEnum DequeueRequiredEnum<TEnum>(this Queue<string> queue) where TEnum : struct => DequeueEnum<TEnum>(queue) ??
            throw new Exception("Oczekiwano wartości enum.");

        public static bool DequeueRequiredBool(this Queue<string> queue, string trueValue, string falseValue)
        {
            var rawValue = queue.Dequeue();
            if (rawValue == trueValue) return true;
            if (rawValue == falseValue) return false;
            throw new Exception($"Oczekiwano wartości {trueValue} lub {falseValue}.");
        }

        public static TEnum? DequeueEnum<TEnum>(this Queue<string> queue) where TEnum : struct
        {
            var rawValue = queue.Dequeue();
            return NMEAParser.ParseEnum<TEnum>(rawValue);
        }
    }
}