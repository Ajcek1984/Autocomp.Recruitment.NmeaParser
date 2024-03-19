using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Autocomp.Nmea.TestApp.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ("negate".Equals(parameter))
                return value != null ? Visibility.Collapsed : Visibility.Visible;

            return value != null ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
