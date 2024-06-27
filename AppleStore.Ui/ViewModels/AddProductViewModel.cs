using System.IO;
using System.Windows.Input;
using Microsoft.Win32;
using AppleStore.Ui.Interfaces;
using AppleStore.Ui.Models;

namespace AppleStore.Ui.ViewModels
{
    public class AddProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        private readonly StoreViewModel _storeViewModel;
        private string _name;
        private decimal _price;
        private byte[] _image;
        private bool _isLoading;

        public AddProductViewModel(IProductService productService, StoreViewModel storeViewModel)
        {
            _productService = productService;
            _storeViewModel = storeViewModel;
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        public byte[] Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChooseImageCommand => new RelayCommand(ChooseImage);
        public ICommand AddProductCommand => new RelayCommand(async () => await AddProduct());

        private async Task ChooseImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                Image = File.ReadAllBytes(openFileDialog.FileName);
            }
            await Task.CompletedTask;
        }

        private async Task AddProduct()
        {
            IsLoading = true;
            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Price = Price,
                Image = Image
            };

            await _productService.AddProductAsync(newProduct);
            await _storeViewModel.LoadProductsAsync();
            IsLoading = false;
        }
    }
}