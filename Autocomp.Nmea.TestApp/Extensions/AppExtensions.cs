using System.Windows;

namespace Autocomp.Nmea.TestApp.Extensions
{
    public static class Dependencies
    {
        public static IServiceProvider ServiceProvider => ((App)Application.Current).DependencyScope.ServiceProvider;
    }
}
