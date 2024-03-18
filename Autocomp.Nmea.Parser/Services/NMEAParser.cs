﻿using Autocomp.Nmea.Common;
using Autocomp.Nmea.Parser.Annotations;
using Autocomp.Nmea.Parser.Extensions;
using Autocomp.Nmea.Parser.Services.FastParsingStrategies;
using System.Globalization;
using System.Reflection;

namespace Autocomp.Nmea.Parser.Services
{
    public class NMEAParser
    {
        private readonly IEnumerable<IFastNMEAParsingStrategy> fastStrategies;

        private Lazy<IDictionary<string, Type>> messageTypes = new Lazy<IDictionary<string, Type>>(() =>
            typeof(NMEAParser).Assembly.GetTypes()
            .Where(t => !t.IsAbstract)
            .Select(t => new
            {
                Attribute = t.GetCustomAttribute<NMEAMessageAttribute>(),
                Type = t
            })
            .Where(t => t.Attribute != null)
            .ToDictionary(t => t.Attribute!.Identifier, t => t.Type));

        public NMEAParser(IEnumerable<IFastNMEAParsingStrategy> fastStrategies)
        {
            this.fastStrategies = fastStrategies;
        }

        public static TEnum? ParseEnum<TEnum>(string rawValue) where TEnum : struct => (TEnum?)ParseEnum(typeof(TEnum), rawValue);

        public object Parse(string message) => Parse(new NmeaMessage(message));

        public object Parse(NmeaMessage message)
        {
            if (message.Header.Length < 4)
                throw new InvalidDataException("Nagłówek jest za krótki");

            var identifier = message.Header[3..];
            var queue = new Queue<string>(SanitizeValues(message.Fields, message.Format.Suffix));
            var fastStrategy = fastStrategies.FirstOrDefault(s => s.Identifier == identifier);
            if (fastStrategy != null)
                return fastStrategy.Parse(queue);

            if (!messageTypes.Value.ContainsKey(identifier))
                throw new NotSupportedException($"Wiadomość o identyfikatorze {identifier} nie jest obsługiwana.");

            var type = messageTypes.Value[identifier];
            var properties = type.GetProperties()
                .Select(p => new { Attribute = p.GetCustomAttribute<NMEAFieldAttribute>(), Property = p })
                .Where(p => p.Attribute != null)
                .Select(p => new { Attribute = p.Attribute!, p.Property })
                .OrderBy(p => p.Attribute.Order)
                .ToArray(); //TODO: Cache

            var parsedMessage = Activator.CreateInstance(type)!;

            foreach (var property in properties)
            {
                if (queue.Count == 0)
                    break;

                var rawValue = queue.Dequeue();
                var parsedValue = GetParsedValue(rawValue, property.Attribute, property.Property.PropertyType);
                if (parsedValue != null)
                    property.Property.SetValue(parsedMessage, parsedValue);
            }

            return parsedMessage;
        }

        private static object? GetParsedValue(string rawValue, NMEAFieldAttribute attribute, Type propertyType)
        {
            var customParser = attribute as ICustomNMEAFieldParser;
            if (customParser != null)
                return customParser.Parse(rawValue, propertyType);

            if (propertyType == typeof(decimal) || propertyType == typeof(decimal?))
                return decimal.Parse(rawValue, CultureInfo.InvariantCulture);

            if (propertyType == typeof(double) || propertyType == typeof(double?))
                return double.Parse(rawValue, CultureInfo.InvariantCulture);

            if (propertyType == typeof(float) || propertyType == typeof(float?))
                return float.Parse(rawValue, CultureInfo.InvariantCulture);

            if (propertyType.IsEnum)
                return ParseEnum(propertyType, rawValue);

            return null;
        }

        private static object? ParseEnum(Type enumType, string rawValue)
        {
            var value = enumType.GetEnumValues()
                    .Cast<Enum>()
                    .Select(e => new
                    {
                        Value = e,
                        Attribute = e.GetAttribute<NMEAEnumValueAttribute>()
                    })
                    .FirstOrDefault(a => a.Attribute?.Value == rawValue);
            return value?.Value;
        }

        private static IEnumerable<string> SanitizeValues(IEnumerable<string> values, char suffix)
        {
            foreach (var rawValue in values)
            {
                if (rawValue.Contains(suffix))
                    yield return rawValue.Substring(0, rawValue.IndexOf(suffix));
                else
                    yield return rawValue;
            }
        }
    }
}