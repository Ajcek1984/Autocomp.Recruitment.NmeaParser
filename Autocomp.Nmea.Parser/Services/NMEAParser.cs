using Autocomp.Nmea.Common;
using Autocomp.Nmea.Parser.Annotations;
using System.Reflection;

namespace Autocomp.Nmea.Parser.Services
{
    public class NMEAParser
    {
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

        public object Parse(string message) => Parse(new NmeaMessage(message));

        public object Parse(NmeaMessage message)
        {
            if (message.Header.Length < 4)
                throw new InvalidDataException("Nagłówek jest za krótki");

            var identifier = message.Header[3..];
            if (!messageTypes.Value.ContainsKey(identifier))
                throw new NotSupportedException($"Wiadomość o identyfikatorze {identifier} nie jest obsługiwana.");

            return null;    //TODO
        }
    }
}
