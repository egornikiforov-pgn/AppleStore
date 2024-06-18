using AppleStore.Core.Models;

 
 namespace AppleStore.DataAccess.Interfaces
 {
 public interface ICartItemRepository
    {
        Task<List<CartItem>> GetCartItemsAsync();
        Task AddProductToCartAsync(Guid productId);
        Task RemoveProductFromCartAsync(Guid productId);
        Task<decimal> GetTotalCartValueAsync();
        Task<int> GetTotalCartItemsCountAsync();
        Task ClearCartAsync();
    }
 }