using System.Reflection;
using System.Windows;

namespace Autocomp.Nmea.TestApp.Services.AutoFormStrategies
{
    public interface IAutoFormStrategy
    {
        bool IsRelevant(PropertyInfo property);

        FrameworkElement CreateControl(object model, PropertyInfo property);

        int Order { get; }
    }
}
