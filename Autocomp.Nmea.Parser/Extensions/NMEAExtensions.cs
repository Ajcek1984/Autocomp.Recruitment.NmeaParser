using Autocomp.Nmea.Common;

namespace Autocomp.Nmea.Parser.Extensions
{
    public static class NMEAExtensions
    {
        public static byte GetCrc(this NmeaMessage message)
        {
            var lastSegment = message.Fields.LastOrDefault();
            if (lastSegment == null)
                throw new Exception("Wiadomość jest pusta");

            var indexOfSuffix = lastSegment.IndexOf(message.Format.Suffix);
            if (indexOfSuffix < 0)
                throw new Exception("Wiadomość nie jest kompletna.");

            var rawCrc = lastSegment.Substring(indexOfSuffix + 1).TrimEnd();
            var bytes = Convert.FromHexString(rawCrc);
            if (bytes.Length != 1)
                throw new Exception("CRC powinno mieć 1 bajt.");

            return bytes[0];
        }

        public static string GetTalkerDevice(this NmeaMessage message) => message.Header[1..3];
    }
}