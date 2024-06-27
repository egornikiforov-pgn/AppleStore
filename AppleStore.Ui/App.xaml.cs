using System;
using System.Net.Http;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using AppleStore.Ui.Interfaces;
using AppleStore.Ui.Services;
using AppleStore.Ui.ViewModels;
using System.Diagnostics;

namespace AppleStore.Ui
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        public IServiceProvider ServiceProvider => _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            var baseUri = new Uri("https://localhost:44334/");
            var httpClient = new HttpClient { BaseAddress = baseUri };

            services.AddSingleton(httpClient);
            services.AddSingleton(baseUri);

            services.AddScoped<IProductService, ProductService>(provider =>
            {
                var client = provider.GetRequiredService<HttpClient>();
                return new ProductService(client);
            });

            services.AddScoped<ICartService, CartService>();
            services.AddScoped<StoreViewModel>();
            services.AddScoped<CartViewModel>();
            services.AddScoped<MainWindowViewModel>();

            services.AddScoped<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = mainWindowViewModel;

            mainWindow.Show();
        }
    }
}