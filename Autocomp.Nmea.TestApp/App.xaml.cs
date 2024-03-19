using Autocomp.Nmea.Parser.Extensions;
using Autocomp.Nmea.TestApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Autocomp.Nmea.TestApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceScope DependencyScope { get; }

        public App()
        {
            var services = new ServiceCollection();
            services.AddNMEAParser();
            services.AddScoped<MainWindowViewModel>();
            DependencyScope = services.BuildServiceProvider().CreateScope();
        }


        protected override void OnExit(ExitEventArgs e)
        {
            DependencyScope?.Dispose();
            base.OnExit(e);
        }

    }

}
