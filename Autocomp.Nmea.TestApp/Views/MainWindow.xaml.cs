using Autocomp.Nmea.TestApp.Extensions;
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
            ViewModel = Dependencies.ServiceProvider.GetRequiredService<MainWindowViewModel>();
            DataContext = ViewModel;
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ViewModel.RefreshNMEAPreview();
        }
    }
}