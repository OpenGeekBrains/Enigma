using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Client.WPF.ViewModels.Base
{
    internal class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.ServiceProvider.GetRequiredService<MainViewModel>();
    }
}
