using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Client.WPF.Services.Navigation
{
    public class NavigationService
    {
        private Dictionary<string, Type> windows { get; } = new Dictionary<string, Type>();
 
        private readonly IServiceProvider serviceProvider;
 
        public void Configure(string key, Type windowType) => windows.Add(key, windowType);
 
        public NavigationService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
 
        public async Task ShowAsync(string windowKey, object parameter = null)
        {
            var window = await GetAndActivateWindowAsync(windowKey, parameter);
            window.Show();
        }
 
        public async Task<bool?> ShowDialogAsync(string windowKey, object parameter = null)
        {
            var window = await GetAndActivateWindowAsync(windowKey, parameter);
            return window.ShowDialog();
        }
 
        private async Task<Window> GetAndActivateWindowAsync(string windowKey, object parameter = null)
        {
            var window = serviceProvider.GetRequiredService(windows[windowKey])
                as Window;
 
            if (window?.DataContext is IActivable activable)
            {
                await activable.ActivateAsync(parameter);
            }
 
            return window;
        }
    }
}
