using AppleStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleStore.DataAccess.Entities
{
    public class CartItemProductEntity
    {
            public Guid CartItemId { get; set; }
            public CartItemEntity CartItem { get; set; }
            public Guid ProductId { get; set; }
            public ProductEntity Product { get; set; }
    }
}
