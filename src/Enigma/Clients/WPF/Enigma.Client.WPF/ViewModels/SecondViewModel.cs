using System.Threading.Tasks;
using Enigma.Client.WPF.Services.Navigation;
using GalaSoft.MvvmLight;

namespace Enigma.Client.WPF.ViewModels
{
    public class SecondViewModel : ViewModelBase, IActivable
    {
        private string _Parameter;

        public string Parameter
        {
            get => _Parameter;
            set => Set(ref _Parameter, value);
        }

        public Task ActivateAsync(object parameter)
        {
            Parameter = parameter?.ToString();
            return Task.CompletedTask;
        }
    }
}
