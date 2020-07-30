using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Client.WPF.Services.Navigation
{
    public class NavigationService
    {
        private Dictionary<string, Type> Windows { get; } = new Dictionary<string, Type>();
 
        private readonly IServiceProvider Services;
 
        public void Configure(string key, Type WindowType) => Windows.Add(key, WindowType);
 
        public NavigationService(IServiceProvider Services) => this.Services = Services;

        public async Task ShowAsync(string WindowKey, object Parameter = null)
        {
            var window = await GetAndActivateWindowAsync(WindowKey, Parameter);
            window.Show();
        }
 
        public async Task<bool?> ShowDialogAsync(string WindowKey, object Parameter = null)
        {
            var window = await GetAndActivateWindowAsync(WindowKey, Parameter);
            return window.ShowDialog();
        }
 
        private async Task<Window> GetAndActivateWindowAsync(string WindowKey, object Parameter = null)
        {
            var service = Services.GetRequiredService(Windows[WindowKey]);
            if (!(service is Window { DataContext: var context } window)) return null;
            if (!(context is IActivable activable)) return window;
            await activable.ActivateAsync(Parameter);
            return window;

        }
    }
}
