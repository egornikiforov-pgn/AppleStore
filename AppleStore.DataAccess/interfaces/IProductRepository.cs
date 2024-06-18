using System;
using System.Collections.Generic;
using AppleStore.Core.Models;
using System.Threading.Tasks;

namespace AppleStore.DataAccess.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync(int page, int pageSize);
        Task<Product> GetProductByIdAsync(Guid id);
        Task<int> GetTotalProductCountAsync();
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid id);
    }
}
