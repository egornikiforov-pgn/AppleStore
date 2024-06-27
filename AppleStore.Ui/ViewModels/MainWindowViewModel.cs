using AppleStore.Ui.Interfaces;
using System;
using System.Windows.Input;

namespace AppleStore.Ui.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel(IProductService productService, 
            ICartService cartService)
        {
            Content = new StoreViewModel(this, productService, cartService);
        }


        private BaseViewModel _content;
        public BaseViewModel Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        // Other properties and methods of the ViewModel
    }
}