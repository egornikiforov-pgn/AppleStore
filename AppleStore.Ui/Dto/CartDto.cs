using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleStore.Ui.Dto
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
