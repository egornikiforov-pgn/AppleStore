using AppleStore.Core.Models;
using AppleStore.DataAccess.Repositories;

namespace AppleStore.DataAccess.Interfaces
{
    public interface ICartRepository
    {
        Task<CartItem> GetCartByIdAsync(Guid cartId);
        Task AddProductToCartAsync(Guid cartId, Guid productId);
        Task<List<Product>> GetAllProductsInCartAsync(Guid cartId);
        Task<decimal> GetTotalCartPriceAsync(Guid cartId);
        Task<int> GetTotalProductCountAsync(Guid cartId);
        Task SortCartProductsByPriceAsync(Guid cartId);
        Task SortCartProductsByNameAsync(Guid cartId);
        Task<Guid> CreateCart();
        Task<List<CartItem>> GetAllCarts();
        Task RemoveProduct(Guid idCart, Guid idProduct);
    }
}