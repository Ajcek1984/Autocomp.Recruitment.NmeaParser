using System.Reflection;

namespace Autocomp.Nmea.Parser.Extensions
{
    public static class ReflectionExtensions
    {
        public static TAttribute? GetAttribute<TAttribute>(this Enum enumValue) where TAttribute : Attribute
            => enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .First()
                        .GetCustomAttribute<TAttribute>();


    }
}
