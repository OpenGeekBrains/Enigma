using System;
using System.Windows;
using Enigma.Client.WPF.Services;
using Enigma.Client.WPF.Services.Navigation;
using Enigma.Client.WPF.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Enigma.Client.WPF.Views.Windows;

namespace Enigma.Client.WPF
{
    public partial class App
    {
        private readonly IHost _Host;

        public static IServiceProvider Services { get; private set; }

        public App()
        {
            _Host = Host
               .CreateDefaultBuilder()
               .ConfigureAppConfiguration((host, config) => config
                  .AddJsonFile("AppSettings.local.json", optional: true, reloadOnChange: true))
               .ConfigureServices((host, services) => ConfigureServices(host.Configuration, services))
               .ConfigureLogging(logs =>
               {
                   // Для сервиса логов
               })
               .Build();

            Services = _Host.Services;
        }

        private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));

            // Сервис оконной навигации
            services.AddScoped(Provider =>
            {
                var navigation_service = new NavigationService(Provider);
                navigation_service.Configure(WindowsDictionary.MainWindow, typeof(MainWindow));
                navigation_service.Configure(WindowsDictionary.FirstWindow, typeof(FirstWindow));
                navigation_service.Configure(WindowsDictionary.SecondWindow, typeof(SecondWindow));

                return navigation_service;
            });


            // Для регистрации вью-моделей приложения
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<FirstViewModel>();
            services.AddSingleton<SecondViewModel>();

            // Для регистрации всех окон приложения
            services.AddTransient<MainWindow>();
            services.AddTransient<FirstWindow>();
            services.AddTransient<SecondWindow>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _Host.StartAsync();

            var navigation = Services.GetRequiredService<NavigationService>();

            await navigation.ShowAsync(WindowsDictionary.FirstWindow);

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            using (_Host) await _Host.StopAsync(TimeSpan.FromSeconds(5));
        }
    }
}
