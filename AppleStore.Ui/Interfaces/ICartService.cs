using AppleStore.Ui.Models;
using AppleStore.Ui.Services;
using static AppleStore.Ui.Services.CartService;

namespace AppleStore.Ui.Interfaces
{
    public interface ICartService
    {
        Task<CartItem> GetCart(Guid cartId);
        Task<List<CartItem>> GetAllCart();
        Task<Guid> CreateCartAsync();
        Task<Status> AddProductToCartAsync(Guid cartId, Guid productId);
        Task<ApiResponse<List<Product>>> GetAllProductsInCartAsync(Guid cartId);
        Task<decimal> GetTotalCartPriceAsync(Guid cartId);
        Task<int> GetTotalProductCountAsync(Guid cartId);
        Task RemoveProductFromCartAsync(Guid cartId, Guid productId);
    }
}
