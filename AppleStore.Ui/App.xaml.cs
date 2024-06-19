using AppleStore.Ui.Services;
using AppleStore.ViewModels;
using AppleStoreUi.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Windows;

namespace AppleStore.Ui
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost/")
            };
            services.AddSingleton(httpClient);

            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<ICartService, CartService>();

            services.AddSingleton<MainWindow>();
            services.AddSingleton<StoreViewModel>();
            services.AddSingleton<CartViewModel>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}