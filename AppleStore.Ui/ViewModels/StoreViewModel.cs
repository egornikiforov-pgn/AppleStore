using AppleStore.Ui.Dto;
using AppleStore.Ui.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AppleStore.ViewModels
{
    public class StoreViewModel : INotifyPropertyChanged
    {
        private readonly IProductService _productService;

        public ObservableCollection<ProductDto> Products { get; set; } = new ObservableCollection<ProductDto>();

        public StoreViewModel(IProductService productService)
        {
            _productService = productService;
            LoadProductsCommand = new RelayCommand(async () => await LoadProductsAsync());
        }

        public ICommand LoadProductsCommand { get; }

        private async Task LoadProductsAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
