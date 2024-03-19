using Autocomp.Nmea.Parser.Annotations;
using System.Collections.Concurrent;
using System.Reflection;

namespace Autocomp.Nmea.Parser.Services
{
    public class NMEAPropertyCache
    {
        private readonly ConcurrentDictionary<Type, NMEAPropertyInfo[]> innerCache = new ConcurrentDictionary<Type, NMEAPropertyInfo[]>();

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
            if (innerCache.ContainsKey(type))
                return innerCache[type];

            var properties = type.GetProperties()
                .Select(p => new { Attribute = p.GetCustomAttribute<NMEAFieldAttribute>(), Property = p })
                .Where(p => p.Attribute != null)
                .Select(p => new NMEAPropertyInfo { Attribute = p.Attribute!, Property = p.Property })
                .OrderBy(p => p.Attribute.Order)
                .ToArray();

            innerCache[type] = properties;
            return properties;
        }
    }

    public class NMEAPropertyInfo
    {
        public required NMEAFieldAttribute Attribute { get; set; }
        public required PropertyInfo Property { get; set; }
    }
}
