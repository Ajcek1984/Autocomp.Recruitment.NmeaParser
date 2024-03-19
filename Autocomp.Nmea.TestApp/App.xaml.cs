using Autocomp.Nmea.Parser.Extensions;
using Autocomp.Nmea.TestApp.Services.AutoFormStrategies;
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
            foreach (var type in typeof(IAutoFormStrategy).Assembly.GetTypes().Where(t => !t.IsAbstract && typeof(IAutoFormStrategy).IsAssignableFrom(t)))
                services.AddScoped(typeof(IAutoFormStrategy), type);

            DependencyScope = services.BuildServiceProvider().CreateScope();
        }


        protected override void OnExit(ExitEventArgs e)
        {
            DependencyScope?.Dispose();
            base.OnExit(e);
        }

    }

}
