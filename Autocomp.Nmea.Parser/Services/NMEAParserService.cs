using Autocomp.Nmea.Common;
using Autocomp.Nmea.Parser.Annotations;
using Autocomp.Nmea.Parser.Extensions;
using Autocomp.Nmea.Parser.Resources;
using Autocomp.Nmea.Parser.Services.FastParsingStrategies;
using System.Globalization;
using System.Linq.Expressions;

namespace Autocomp.Nmea.Parser.Services
{
    /// <summary>
    /// Serwis do parsowania wiadomości NMEA 0183. Zasadniczo parsuje wiadomości przy użyciu refleksji. Jeśli wymagana jest wysoka wydajność, można
    /// zaimplementować IFastNMEAParsingStrategy dla danego typu wiadomości - wtedy implementacja ze strategii zostanie użyta zamiast standardowej
    /// implementacji wykorzystyjącej refleksję.
    /// </summary>
    public class NMEAParserService
    {
        private readonly NMEAPropertyCache cache;
        private readonly IEnumerable<IFastNMEAParsingStrategy> fastStrategies;

        public NMEAParserService(IEnumerable<IFastNMEAParsingStrategy> fastStrategies, NMEAPropertyCache cache)
        {
            this.fastStrategies = fastStrategies;
            this.cache = cache;
        }

        /// <summary>
        /// Parsuje wiadomość Nmea do silnie typowanego obiektu.
        /// </summary>
        /// <param name="message">Wiadomość</param>
        /// <param name="disableFastStrategies">Ustaw na true, jeśli mają zostać wyłączone wszystkie strategie optymalizujące (zostanie użyta
        /// wyłącznie implementacja refleksyjna). Zostaw na false, jeśli mają zostać zastosowane strategie optymalizujące (jeśli istnieją
        /// dla danego typu wiadomości).</param>
        /// <returns>Silnie typowany obiekt wiadomości</returns>
        /// <exception cref="InvalidDataException">Wyjątek, jeśli dane są nieprawidłowe.</exception>
        /// <exception cref="NotSupportedException">Wyjątek, jeśli identyfikator wiadomości nie jest obsługiwany.</exception>
        public object Parse(string message, bool disableFastStrategies) => Parse(new NmeaMessage(message), disableFastStrategies);

        /// <summary>
        /// Parsuje wiadomość Nmea do silnie typowanego obiektu.
        /// </summary>
        /// <param name="message">Wiadomość</param>
        /// <param name="disableFastStrategies">Ustaw na true, jeśli mają zostać wyłączone wszystkie strategie optymalizujące (zostanie użyta
        /// wyłącznie implementacja refleksyjna). Zostaw na false, jeśli mają zostać zastosowane strategie optymalizujące (jeśli istnieją
        /// dla danego typu wiadomości).</param>
        /// <returns>Silnie typowany obiekt wiadomości</returns>
        /// <exception cref="InvalidDataException">Wyjątek, jeśli dane są nieprawidłowe.</exception>
        /// <exception cref="NotSupportedException">Wyjątek, jeśli identyfikator wiadomości nie jest obsługiwany.</exception>
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
            var queue = new Queue<string>(SanitizeValues(message.Fields, message.Format.Suffix));
            var fastStrategy = !disableFastStrategies ? fastStrategies.FirstOrDefault(s => s.Identifier == identifier) : null;
            if (fastStrategy != null)
                return ApplyTalkerDevice(fastStrategy.Parse(queue), message);

            if (!cache.MessageTypes.TryGetValue(identifier, out Type? type))
                throw new NotSupportedException(string.Format(CommonResources.MessageTypeNotSupported, identifier));

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
            if (attribute is ICustomNMEAFieldParser customParser)
                return customParser.Parse(rawValue, propertyType);

            if (propertyType == typeof(decimal) || propertyType == typeof(decimal?))
                return decimal.Parse(rawValue, CultureInfo.InvariantCulture);

            if (propertyType == typeof(double) || propertyType == typeof(double?))
                return double.Parse(rawValue, CultureInfo.InvariantCulture);

            if (propertyType == typeof(float) || propertyType == typeof(float?))
                return float.Parse(rawValue, CultureInfo.InvariantCulture);

            if (propertyType == typeof(long) || propertyType == typeof(long?))
                return long.Parse(rawValue, CultureInfo.InvariantCulture);

            if (propertyType == typeof(int) || propertyType == typeof(int?))
                return int.Parse(rawValue, CultureInfo.InvariantCulture);

            if (propertyType == typeof(short) || propertyType == typeof(short?))
                return short.Parse(rawValue, CultureInfo.InvariantCulture);

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
                return NMEAExtensions.ParseEnum(propertyType, rawValue);

            return null;
        }

        /// <summary>
        /// Naprawia wartości pól, usuwając wszystko po symbolu końca wiadomości.
        /// </summary>
        /// <param name="values">Wartości</param>
        /// <param name="suffix">Sufiks</param>
        /// <returns>Wartości</returns>
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

        /// <summary>
        /// Ustawia właściwość urządzenia rejestrującego, jeśli właściwość jest obecna w klasie.
        /// </summary>
        /// <param name="obj">Obiekt silnie typowanej wiadomości</param>
        /// <param name="message">Wiadomość Nmea</param>
        /// <returns>Obiekt silnie typowanej wiadomości</returns>
        private object ApplyTalkerDevice(object obj, NmeaMessage message)
        {
            var talkerDeviceProperty = cache.GetTalkerDeviceProperty(obj.GetType());
            if (talkerDeviceProperty == null)
                return obj;

            var rawTalkerDevice = message.GetTalkerDevice();
            if (string.IsNullOrWhiteSpace(rawTalkerDevice))
                return obj;

            if (talkerDeviceProperty.PropertyType.IsEnum)
                talkerDeviceProperty.SetValue(obj, NMEAExtensions.ParseEnum(talkerDeviceProperty.PropertyType, rawTalkerDevice));
            else if (talkerDeviceProperty.PropertyType == typeof(string))
                talkerDeviceProperty.SetValue(obj, rawTalkerDevice);

            return obj;
        }
    }
}