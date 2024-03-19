using Autocomp.Nmea.TestApp.Extensions;
using Autocomp.Nmea.TestApp.Services.AutoFormStrategies;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Autocomp.Nmea.TestApp.Controls
{
    public abstract class AutoFormControl : UserControl
    {
        protected abstract StackPanel MainStackPanel { get; }

        protected void BindProperties(object model)
        {
            if (model == null) return;
            var properties = model.GetType().GetProperties().Where(p => p.GetCustomAttribute<DisplayAttribute>() != null).ToArray();
            var strategies = Dependencies.ServiceProvider.GetRequiredService<IEnumerable<IAutoFormStrategy>>().OrderBy(s => s.Order).ToArray();

            foreach (var property in properties)
            {
                var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
                if (!displayAttribute?.GetAutoGenerateField() ?? false) continue;

                var relevantStrategy = strategies.FirstOrDefault(s => s.IsRelevant(property));
                if (relevantStrategy == null) continue;

                var grid = new Grid
                {
                    Margin = new Thickness(8),
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(200) });
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                var label = new TextBlock
                {
                    Text = property.GetCustomAttribute<DisplayAttribute>()?.GetName() ?? property.Name,
                    TextWrapping = TextWrapping.Wrap
                };
                Grid.SetColumn(label, 0);
                grid.Children.Add(label);

                var control = relevantStrategy.CreateControl(model, property);
                control.IsEnabled = IsEnabled;
                Grid.SetColumn(control, 1);
                Grid.SetColumnSpan(control, 2);
                grid.Children.Add(control);

                MainStackPanel.Children.Add(grid);
            }
        }

    }
}
