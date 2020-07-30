using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Client.WPF.ViewModels.Base
{
    internal class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.Services.GetRequiredService<MainViewModel>();

        public FirstViewModel FirstViewModel => App.Services.GetRequiredService<FirstViewModel>();

        public SecondViewModel SecondViewModel => App.Services.GetRequiredService<SecondViewModel>();
    }
}
