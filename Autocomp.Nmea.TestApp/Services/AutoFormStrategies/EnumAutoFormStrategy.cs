using Autocomp.Nmea.Parser.Extensions;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Autocomp.Nmea.TestApp.Services.AutoFormStrategies
{
    public class EnumAutoFormStrategy : IAutoFormStrategy
    {
        public int Order => 3;

        public FrameworkElement CreateControl(object model, PropertyInfo property)
        {
            var comboBox = new ComboBox
            {
                ItemsSource = property.PropertyType.GetEnumValues().Cast<Enum>().ToDictionary(e => e, e => e.GetDisplayName()),
                SelectedValuePath = "Key",
                DisplayMemberPath = "Value"
            };

            comboBox.SetBinding(Selector.SelectedValueProperty, new Binding()
            {
                Path = new PropertyPath(property.Name),
                Source = model
            });

            return comboBox;
        }

        public bool IsRelevant(PropertyInfo property) =>
            property.PropertyType.IsEnum;
    }
}
