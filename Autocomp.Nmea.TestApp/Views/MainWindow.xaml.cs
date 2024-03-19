using Autocomp.Nmea.TestApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Autocomp.Nmea.TestApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = ((App)Application.Current).DependencyScope.ServiceProvider.GetRequiredService<MainWindowViewModel>();
            DataContext = ViewModel;
        }
    }
}