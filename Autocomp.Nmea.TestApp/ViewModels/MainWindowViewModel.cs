using Autocomp.Nmea.Parser.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Autocomp.Nmea.TestApp.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly NMEAParserService parserService;

        private string? errorMessage;
        private string nmeaMessage = string.Empty;

        private object? parsedMessage;
        public MainWindowViewModel(NMEAParserService parserService)
        {
            this.parserService = parserService;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string? ErrorMessage
        {
            get => errorMessage;
            set { errorMessage = value; RaisePropertyChanged(); }
        }

        public string NmeaMessage
        {
            get => nmeaMessage;
            set { nmeaMessage = value; RaisePropertyChanged(); }
        }

        public object? ParsedMessage
        {
            get => parsedMessage;
            set { parsedMessage = value; RaisePropertyChanged(); }
        }
        public void RefreshNMEAPreview()
        {
            try
            {
                ParsedMessage = parserService.Parse(NmeaMessage, false);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Parsowanie nieudane. Treść błędu: {ex.Message}{(ex.Message.EndsWith(".") ? "" : ".")}";
                ParsedMessage = null;
            }
        }

        protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}