using System.ComponentModel.DataAnnotations;
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

        public static string GetDisplayName(this Enum value)
        {
            var fieldInfo = value.GetType().GetRuntimeField(value.ToString());
            return fieldInfo?.GetCustomAttribute<DisplayAttribute>()?.GetName() ?? fieldInfo?.Name ?? value.ToString();
        }
    }
}
