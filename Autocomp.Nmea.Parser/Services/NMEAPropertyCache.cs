using Autocomp.Nmea.Parser.Annotations;
using System.Collections.Concurrent;
using System.Reflection;

namespace Autocomp.Nmea.Parser.Services
{
    /// <summary>
    /// Pomocnicza klasa stanowiąca cache właściwości pobranych za pomocą refleksji. Rejestrować jako singleton lub w szerokim skołpie.
    /// </summary>
    public class NMEAPropertyCache
    {
        private readonly ConcurrentDictionary<Type, NMEAPropertyInfo[]> propertyCache = new ConcurrentDictionary<Type, NMEAPropertyInfo[]>();
        private readonly ConcurrentDictionary<Type, PropertyInfo?> talkerDevicePropertyCache = new ConcurrentDictionary<Type, PropertyInfo?>();

        private Lazy<IDictionary<string, Type>> messageTypes = new Lazy<IDictionary<string, Type>>(() =>
                    typeof(NMEAParserService).Assembly.GetTypes()
            .Where(t => !t.IsAbstract)
            .Select(t => new
            {
                Attribute = t.GetCustomAttribute<NMEAMessageAttribute>(),
                Type = t
            })
            .Where(t => t.Attribute != null)
            .ToDictionary(t => t.Attribute!.Identifier, t => t.Type));
        public IDictionary<string, Type> MessageTypes => messageTypes.Value;

        public NMEAPropertyInfo[] GetProperties(Type type)
        {
            if (propertyCache.ContainsKey(type))
                return propertyCache[type];

            var properties = type.GetProperties()
                .Select(p => new { Attribute = p.GetCustomAttribute<NMEAFieldAttribute>(), Property = p })
                .Where(p => p.Attribute != null)
                .Select(p => new NMEAPropertyInfo { Attribute = p.Attribute!, Property = p.Property })
                .OrderBy(p => p.Attribute.Order)
                .ToArray();

            propertyCache[type] = properties;
            return properties;
        }

        public PropertyInfo? GetTalkerDeviceProperty(Type type)
        {
            if (talkerDevicePropertyCache.ContainsKey(type))
                return talkerDevicePropertyCache[type];

            var property = type.GetProperties().FirstOrDefault(p => p.GetCustomAttribute<NMEATalkerDeviceAttribute>() != null);
            talkerDevicePropertyCache[type] = property;

            return property;
        }
    }

    public class NMEAPropertyInfo
    {
        public required NMEAFieldAttribute Attribute { get; set; }
        public required PropertyInfo Property { get; set; }
    }
}
