﻿using Autocomp.Nmea.Common;
using Autocomp.Nmea.Parser.Annotations;
using Autocomp.Nmea.Parser.Extensions;
using Autocomp.Nmea.Parser.Resources;
using Autocomp.Nmea.Parser.Services.FastParsingStrategies;
using System.Globalization;

namespace Autocomp.Nmea.Parser.Services
{
    public class NMEAParserService
    {
        private readonly NMEAPropertyCache cache;
        private readonly IEnumerable<IFastNMEAParsingStrategy> fastStrategies;

        public NMEAParserService(IEnumerable<IFastNMEAParsingStrategy> fastStrategies, NMEAPropertyCache cache)
        {
            this.fastStrategies = fastStrategies;
            this.cache = cache;
        }

        public static TEnum? ParseEnum<TEnum>(string rawValue) where TEnum : struct => (TEnum?)ParseEnum(typeof(TEnum), rawValue);

        public object Parse(string message, bool disableFastStrategies) => Parse(new NmeaMessage(message), disableFastStrategies);

        public object Parse(NmeaMessage message, bool disableFastStrategies)
        {
            if (!message.Header.StartsWith(message.Format.Prefix))
                throw new InvalidDataException(string.Format(CommonResources.MessageShouldBePrefixedWith, message.Format.Prefix));

            if (message.Header.Length < 4)
                throw new InvalidDataException(CommonResources.HeaderIsTooShort);

            var calculatedCrc = message.CalculateCrc();
            var embeddedCrc = message.GetCrc();
            if (calculatedCrc != embeddedCrc)
                throw new InvalidDataException(CommonResources.InvalidCRC);

            var identifier = message.Header[3..];
            var talkerDevice = message.GetTalkerDevice();
            var queue = new Queue<string>(SanitizeValues(message.Fields, message.Format.Suffix));
            var fastStrategy = !disableFastStrategies ? fastStrategies.FirstOrDefault(s => s.Identifier == identifier) : null;
            if (fastStrategy != null)
                return ApplyTalkerDevice(fastStrategy.Parse(queue), message);

            if (!cache.MessageTypes.ContainsKey(identifier))
                throw new NotSupportedException(string.Format(CommonResources.MessageTypeNotSupported, identifier));

            var type = cache.MessageTypes[identifier];
            var properties = cache.GetProperties(type);

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

            return ApplyTalkerDevice(parsedMessage, message);
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

            if (propertyType == typeof(TimeSpan))
            {
                var splitValue = rawValue.Split('.');
                var res = TimeSpan.ParseExact(splitValue[0], "hhmmss", CultureInfo.InvariantCulture);
                if (splitValue.Length > 1)
                {
                    if (!int.TryParse(splitValue[1], out int i))
                        throw new Exception(CommonResources.IncorrectlyFormattedDecimalValue);
                    res = res.Add(TimeSpan.FromSeconds(i * Math.Pow(0.1d, splitValue[1].Length)));
                }
                return res;
            }

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

        private object ApplyTalkerDevice(object obj, NmeaMessage message)
        {
            var talkerDeviceProperty = cache.GetTalkerDeviceProperty(obj.GetType());
            if (talkerDeviceProperty == null)
                return obj;

            var rawTalkerDevice = message.GetTalkerDevice();
            if (string.IsNullOrWhiteSpace(rawTalkerDevice))
                return obj;

            if (talkerDeviceProperty.PropertyType.IsEnum)
                talkerDeviceProperty.SetValue(obj, ParseEnum(talkerDeviceProperty.PropertyType, rawTalkerDevice));
            else if (talkerDeviceProperty.PropertyType == typeof(string))
                talkerDeviceProperty.SetValue(obj, rawTalkerDevice);

            return obj;
        }
    }
}