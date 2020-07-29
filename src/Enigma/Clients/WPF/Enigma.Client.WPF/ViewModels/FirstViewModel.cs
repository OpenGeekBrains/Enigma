using System.Diagnostics;
using System.Threading.Tasks;
using Enigma.Client.WPF.Services;
using Enigma.Client.WPF.Services.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.Options;

namespace Enigma.Client.WPF.ViewModels
{
    public class FirstViewModel : ViewModelBase
    {
        private readonly NavigationService _navigationService;
        
        public string Title { get; set; }

        //для проверки навигационного сервиса
        private string input;
        public string Input
        {
            get => input;
            set => Set(ref input, value);
        }

        private RelayCommand _executeCommand;
        public RelayCommand ExecuteCommand => 
            _executeCommand ?? new RelayCommand(async () => await ExecuteAsync());

        private Task ExecuteAsync()
        {
            Debug.WriteLine($"Current value: {input}");
            return _navigationService.ShowDialogAsync(WindowsDictionary.SecondWindow, input);
        }
        //для проверки навигационного сервиса

        public FirstViewModel(NavigationService navigationService, IOptions<AppSettings> options)
        {
            _navigationService = navigationService;
            Title = options.Value.MainWindowTitle;
        }
    }
}
