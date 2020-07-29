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
        private readonly IHost host;

        public static IServiceProvider ServiceProvider { get; private set; }
 
        public App()
        {
            host = Host.CreateDefaultBuilder()  
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddJsonFile("AppSettings.local.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(context.Configuration, services);
                })
                .ConfigureLogging(logging => 
                {
                    // Для сервиса логов
                })
                .Build();

            ServiceProvider = host.Services;
        }
 
        private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
            
            // Сервис оконной навигации
            services.AddScoped<NavigationService>(serviceProvider =>
            {
                var navigationService = new NavigationService(serviceProvider);
                navigationService.Configure(WindowsDictionary.MainWindow, typeof(MainWindow));
                navigationService.Configure(WindowsDictionary.FirstWindow, typeof(FirstWindow));
                navigationService.Configure(WindowsDictionary.SecondWindow, typeof(SecondWindow));
 
                return navigationService;
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
            await host.StartAsync();

            var navigationService = ServiceProvider.GetRequiredService<NavigationService>();
 
            await navigationService.ShowAsync(WindowsDictionary.FirstWindow);
 
            base.OnStartup(e);
        }
 
        protected override async void OnExit(ExitEventArgs e)
        {
            using (host)
            {
                await host.StopAsync(TimeSpan.FromSeconds(5));
            }
 
            base.OnExit(e);
        }
    }
}
