using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AppleStore.Ui.Services;
using AppleStore.Ui.Dto;

namespace AppleStoreUi.ViewModels
{
    public class CartViewModel : INotifyPropertyChanged
    {
        private readonly ICartService _cartService;

        public ObservableCollection<ProductDto> CartProducts { get; set; } = new ObservableCollection<ProductDto>();
        private decimal _totalPrice;
        private int _totalCount;

        public CartViewModel(ICartService cartService)
        {
            _cartService = cartService;
            LoadCartCommand = new RelayComand(async () => await LoadCartAsync());
        }

        public ICommand LoadCartCommand { get; }

        public decimal TotalPrice
        {
            get => _totalPrice;
            set
            {
                _totalPrice = value;
                OnPropertyChanged();
            }
        }

        public int TotalCount
        {
            get => _totalCount;
            set
            {
                _totalCount = value;
                OnPropertyChanged();
            }
        }

        private async Task LoadCartAsync()
        {
            var cart = await _cartService.GetCartAsync();
            CartProducts.Clear();
            foreach (var product in cart.Products)
            {
                CartProducts.Add(product);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
