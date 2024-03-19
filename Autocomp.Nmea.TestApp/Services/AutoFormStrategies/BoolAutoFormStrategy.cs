using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Autocomp.Nmea.TestApp.Services.AutoFormStrategies
{
    public class BoolAutoFormStrategy : IAutoFormStrategy
    {
        public int Order => 4;

        public FrameworkElement CreateControl(object model, PropertyInfo property)
        {
            var checkbox = new CheckBox();
            if (property.PropertyType == typeof(bool?)) checkbox.IsThreeState = true;
            checkbox.SetBinding(ToggleButton.IsCheckedProperty, new Binding()
            {
                Path = new PropertyPath(property.Name),
                Source = model
            });
            return checkbox;
        }

        public bool IsRelevant(PropertyInfo property) =>
            property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?);
    }
}
