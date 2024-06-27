using AppleStore.Ui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleStore.Ui.ViewModels
{
    public class ProductShortViewModel
    {
        private Product _theater;

        public ProductShortViewModel(Product theater)
        {
            _theater = theater;
        }

        public string Name
        {
            get => _theater.Name;
        }
    }
}
