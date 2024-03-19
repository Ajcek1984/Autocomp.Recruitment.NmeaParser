using Autocomp.Nmea.Parser.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Autocomp.Nmea.TestApp.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly NMEAParserService parserService;

        public MainWindowViewModel(NMEAParserService parserService)
        {
            this.parserService = parserService;
        }

        private string nmeaMessage = string.Empty;

        public string NmeaMessage
        {
            get => nmeaMessage;
            set { nmeaMessage = value; RaisePropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void RefreshNMEAPreview()
        {
            //TODO
        }

        protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}