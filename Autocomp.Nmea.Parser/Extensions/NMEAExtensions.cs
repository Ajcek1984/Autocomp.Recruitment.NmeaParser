using Autocomp.Nmea.Common;
using Autocomp.Nmea.Parser.Resources;

namespace Autocomp.Nmea.Parser.Extensions
{
    public static class NMEAExtensions
    {
        /// <summary>
        /// Poprawiona wersja metody NmeaCrcCalculator.CRC, która nie działa poprawnie, bo bierze pod uwagę także sufiks i prefiks.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static byte CalculateCrc(this NmeaMessage msg)
        {
            byte crc = 0;

            var headerWithoutPrefix = msg.Header.Replace(msg.Format.Prefix.ToString(), "");
            if (!string.IsNullOrEmpty(headerWithoutPrefix))
            {
                for (int i = 0; i < headerWithoutPrefix.Length; i++)
                {
                    crc ^= (byte)headerWithoutPrefix[i];
                }
            }

            if (msg.Fields != null)
            {
                foreach (string field in msg.Fields)
                {
                    var sanitizedField = field;
                    if (sanitizedField.Contains(msg.Format.Suffix))
                        sanitizedField = sanitizedField.Substring(0, sanitizedField.IndexOf(msg.Format.Suffix));

                    crc ^= Convert.ToByte(msg.Format.Separator);
                    if (!string.IsNullOrEmpty(sanitizedField))
                    {
                        for (int i = 0; i < sanitizedField.Length; i++)
                        {
                            crc ^= (byte)sanitizedField[i];
                        }
                    }
                }
            }

            return crc;
        }

        public static byte GetCrc(this NmeaMessage message)
        {
            var lastSegment = message.Fields.LastOrDefault();
            if (lastSegment == null)
                throw new Exception(CommonResources.MessageIsEmpty);

            var indexOfSuffix = lastSegment.IndexOf(message.Format.Suffix);
            if (indexOfSuffix < 0)
                throw new Exception(string.Format(CommonResources.MessageShouldBeSuffixedWith, message.Format.Suffix));

            if (lastSegment.Length < indexOfSuffix + 3)
                throw new Exception(CommonResources.InvalidCRSSize);

            var rawCrc = lastSegment.Substring(indexOfSuffix + 1, 2);
            var bytes = Convert.FromHexString(rawCrc);
            if (bytes.Length != 1)
                throw new Exception(CommonResources.InvalidCRSSize);

            return bytes[0];
        }

        public static string GetTalkerDevice(this NmeaMessage message) => message.Header[1..3];
    }
}