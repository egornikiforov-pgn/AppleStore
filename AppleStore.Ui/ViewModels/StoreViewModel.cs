using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AppleStore.Ui.Interfaces;
using AppleStore.Ui.Models;

namespace AppleStore.Ui.ViewModels
{
    public class StoreViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly MainWindowViewModel _mwVm;

        public StoreViewModel(MainWindowViewModel mwVm, IProductService productService, ICartService cartService)
        {
            _mwVm = mwVm;
            _productService = productService;
            _cartService = cartService;
            _products = new ObservableCollection<Product>();
            _idCart = new Guid("e413aff5-ffc5-4e09-a423-9da06ba6e5f5");
            LoadProductsAsync();
        }

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                if (_selectedProduct != value)
                {
                    _selectedProduct = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task LoadProductsAsync()
        {
            var products = await _productService.GetProductsAsync(1, 100);
            Products.Clear();
            foreach (var product in products.Data)
            {
                Products.Add(product);
            }
        }

        private Guid _idCart;
        private async Task<Guid> CreateCartAsync()
        {
            _idCart = await _cartService.CreateCartAsync();
            return _idCart;
        }

        public ICommand AddToCartCommand => new RelayCommand<Product>(async (product) => await AddToCartAsync(product));

        private async Task AddToCartAsync(Product product)
        {
            if (product == null)
                return;

            var result = await _cartService.AddProductToCartAsync(_idCart, product.Id);
            if (result.Item == 1) product.Count += 1;
            // Обновите список продуктов в корзине
            _mwVm.Content = new CartViewModel(_idCart, _mwVm, _productService, _cartService);
        }

        private async Task ToCartImpl()
        {
            var cartViewModel = new CartViewModel(_idCart, _mwVm, _productService, _cartService);
            await cartViewModel.LoadProductsAsync(); // Обновите список продуктов в корзине перед переходом
            _mwVm.Content = cartViewModel;
            await Task.CompletedTask;
        }

        public ICommand ToCartCommand => new RelayCommand(ToCartImpl);

        private async Task OpenAddProductWindow()
        {
            //var addProductWindow = new AddProductViewModel(_mwVm, _productService, _cartService);
            await Task.CompletedTask;
        }

        public ICommand AddProductCommand => new RelayCommand(AddProduct);

        private async Task AddProduct()
        {
            var addProductDialog = new AddProductDialog();
            if (addProductDialog.ShowDialog() == true)
            {
                await _productService.AddProductAsync(addProductDialog.NewProduct);
                Products.Add(addProductDialog.NewProduct);
            }
            await Task.CompletedTask;
        }
    }
}