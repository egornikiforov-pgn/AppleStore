using AppleStore.Ui.Models;
using AppleStore.Ui.Services;

namespace AppleStore.Ui.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts(int page, int pageSize);
        Task<ApiResponse<List<Product>>> GetProductsAsync(int page, int pageSize);
        Task<Product> GetProductByIdAsync(Guid id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid id);
    }
}
