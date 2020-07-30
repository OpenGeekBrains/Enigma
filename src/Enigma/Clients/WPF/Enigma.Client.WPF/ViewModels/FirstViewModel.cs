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
        private readonly NavigationService _NavigationService;
        
        public string Title { get; set; }

        //для проверки навигационного сервиса
        private string _Input;

        public string Input
        {
            get => _Input;
            set => Set(ref _Input, value);
        }

        private RelayCommand _ExecuteCommand;

        public RelayCommand ExecuteCommand => 
            _ExecuteCommand ??= new RelayCommand(async () => await ExecuteAsync());

        private Task ExecuteAsync()
        {
            Debug.WriteLine($"Current value: {_Input}");
            return _NavigationService.ShowDialogAsync(WindowsDictionary.SecondWindow, _Input);
        }
        //для проверки навигационного сервиса

        public FirstViewModel(NavigationService navigationService, IOptions<AppSettings> options)
        {
            _NavigationService = navigationService;
            Title = options.Value.MainWindowTitle;
        }
    }
}
