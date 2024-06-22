using AppleStore.Core.Models;
using AppleStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppleStore.ApplicationLayer.Interfaces
{
    public interface ICartItemServices
    {
        Task RemoveProduct(Guid cartId, Guid productId);
        Task<CartItem> GetCartByIdAsync(Guid cartId);
        Task AddProductToCartAsync(Guid cartId, Guid productId);
        Task<List<Product>> GetAllProductsInCartAsync(Guid cartId);
        Task<decimal> GetTotalCartPriceAsync(Guid cartId);
        Task<int> GetTotalProductCountAsync(Guid cartId);
        Task SortCartProductsByPriceAsync(Guid cartId);
        Task SortCartProductsByNameAsync(Guid cartId);
        Task<Guid> CreateCart();
        Task<List<CartItem>> GetAllCarts();


    }
}
