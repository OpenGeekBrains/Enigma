using System;
using System.Windows;
using Enigma.Client.WPF.Services;
using Enigma.Client.WPF.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            
            // Для регистрации вью-моделей приложения
            services.AddSingleton<MainViewModel>();
 
            // Для регистрации всех окон приложения
            services.AddTransient<MainWindow>();
        }
 
        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();
 
            var mainWindow = host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
 
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
