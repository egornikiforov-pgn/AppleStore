using AppleStore.Core.Models;

namespace AppleStore.ApplicationLayer.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync(int page, int pageSize);
        Task<Product> GetProductByIdAsync(Guid id);
        Task<int> GetTotalProductCountAsync();
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid id);
    }
}