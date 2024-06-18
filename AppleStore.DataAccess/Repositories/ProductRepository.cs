using AppleStore.DataAccess.Entities;
using AppleStore.Core.Models;
using AppleStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using AppleStore.DataAccess.Exceptions;


namespace AppleStore.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppleStoreDbContext _dbContext;

        public ProductRepository(AppleStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetProductsAsync(int page, int pageSize)
        {       
            return await _dbContext.Products
                .OrderBy(p => p.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new Product(p.Id, p.Name ?? string.Empty, p.Price, p.Image))
                .ToListAsync();
    }


     public async Task<Product> GetProductByIdAsync(Guid id)
    {
        var productEntity = await _dbContext.Products.FindAsync(id);
        if (productEntity == null)
        {
            throw new NotFoundException($"Product with ID {id} was not found.");
        }

        return new Product(
            productEntity.Id,
            productEntity.Name ?? string.Empty,
            productEntity.Price,
            productEntity.Image ?? Array.Empty<byte>()
        );
    }

        public async Task<int> GetTotalProductCountAsync()
        {
            int? count = await _dbContext.Products?.CountAsync();
            return count ?? 0;
        }



        public async Task AddProductAsync(Product product)
        {
            var productEntity = new ProductEntity
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Image = product.Image
            };

            _dbContext.Products.Add(productEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            var productEntity = await _dbContext.Products.FindAsync(product.Id);
            if (productEntity == null)
            {
                throw new NotFoundException($"Product with ID {product.Id} was not found.");
            }

            productEntity.Name = product.Name;
            productEntity.Price = product.Price;
            productEntity.Image = product.Image;

            _dbContext.Products.Update(productEntity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(Guid id)
        {
            var productEntity = await _dbContext.Products.FindAsync(id);
            

            _dbContext.Products.Remove(
                productEntity ?? throw new NotFoundException($"Product with ID {id} was not found."));
            await _dbContext.SaveChangesAsync();
        }

    }
}
