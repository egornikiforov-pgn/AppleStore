using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AppleStore.Ui.Interfaces;
using AppleStore.Ui.Models;

namespace AppleStore.Ui.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly MainWindowViewModel _mwVM;
        private Guid _idCart;

        public CartViewModel(Guid idCart, MainWindowViewModel mwVm, IProductService productService, ICartService cartService)
        {
            _cartService = cartService;
            _productService = productService;
            _idCart = idCart;
            _mwVM = mwVm;
            LoadProductsAsync(); // Загрузите продукты при создании модели представления
        }
        public decimal TotalPrice => CartProducts?.Sum(p => p.Price) ?? 0;

        public int TotalQuantity => CartProducts?.Count ?? 0;

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> CartProducts
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(CartProducts));
                OnPropertyChanged(nameof(TotalPrice));
                OnPropertyChanged(nameof(TotalQuantity));
            }
        }

        public async Task LoadProductsAsync()
        {
            var api = await _cartService.GetAllProductsInCartAsync(_idCart);
            CartProducts = new ObservableCollection<Product>(api.Data);
        }

        public ICommand ToStoreCommand => new RelayCommand(async () => await ToStoreImpl());

        private async Task ToStoreImpl()
        {
            var storeViewModel = new StoreViewModel(_mwVM, _productService, _cartService);
            await storeViewModel.LoadProductsAsync();
            _mwVM.Content = storeViewModel;
            await Task.CompletedTask;
        }
        public ICommand RemoveFromCartCommand => new RelayCommand<Product>(async product => await RemoveFromCart(product));
        private async Task RemoveFromCart(Product product)
        {
            await _cartService.RemoveProductFromCartAsync(_idCart, product.Id);
            CartProducts.Remove(product);
        }
    }
}
