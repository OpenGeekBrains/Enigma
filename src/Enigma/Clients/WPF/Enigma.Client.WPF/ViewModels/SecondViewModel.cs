using System.Threading.Tasks;
using Enigma.Client.WPF.Services.Navigation;
using GalaSoft.MvvmLight;

namespace Enigma.Client.WPF.ViewModels
{
    public class SecondViewModel : ViewModelBase, IActivable
    {
        private string parameter;
        public string Parameter
        {
            get => parameter;
            set => Set(ref parameter, value);
        }

        public Task ActivateAsync(object parameter)
        {
            Parameter = parameter?.ToString();
            return Task.CompletedTask;
        }
    }
}
