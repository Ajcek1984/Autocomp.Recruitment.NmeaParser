using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Autocomp.Nmea.TestApp.Services.AutoFormStrategies
{
    public class DefaultAutoFormStrategy : IAutoFormStrategy
    {
        public int Order => int.MaxValue;

        public FrameworkElement CreateControl(object model, PropertyInfo property)
        {
            var textBox = new TextBox();

            textBox.SetBinding(TextBox.TextProperty, new Binding()
            {
                Path = new PropertyPath(property.Name),
                Source = model
            });

            return textBox;
        }

        public bool IsRelevant(PropertyInfo property) => true;
    }
}
