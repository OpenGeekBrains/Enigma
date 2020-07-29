using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Client.WPF.ViewModels.Base
{
    internal class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.ServiceProvider.GetRequiredService<MainViewModel>();
        public FirstViewModel FirstViewModel => App.ServiceProvider.GetRequiredService<FirstViewModel>();
        public SecondViewModel SecondViewModel => App.ServiceProvider.GetRequiredService<SecondViewModel>();
    }
}
