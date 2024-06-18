using AppleStore.DataAccess.Entities;
using AppleStore.Core.Models;
using AppleStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using AppleStore.DataAccess.Exceptions;

namespace AppleStore.DataAccess.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly AppleStoreDbContext _dbContext;

        public CartItemRepository(AppleStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CartItem>> GetCartItemsAsync()
        {
            var cartItems = await _dbContext.CartItems
                .Include(ci => ci.Products)
                .ToListAsync();

            return cartItems.Select(ci => new CartItem(
                ci.Id, 
                ci.Products?.Select(p => new Product(p.Id, p.Name ?? string.Empty, p.Price, p.Image)).ToList() ?? new List<Product>())
            ).ToList();
        }

        public async Task AddProductToCartAsync(Guid productId)
        {
            var productEntity = await _dbContext.Products.FindAsync(productId);
            if (productEntity == null)
            {
                throw new NotFoundException($"Product with ID {productId} was not found.");
            }

            var cartItem = await _dbContext.CartItems.Include(ci => ci.Products).FirstOrDefaultAsync(ci => ci.Products.Any(p => p.Id == productId));
            if (cartItem == null)
            {
                cartItem = new CartItemEntity
                {
                    Products = new List<ProductEntity> { productEntity }
                };
                _dbContext.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Products.Add(productEntity);
                _dbContext.CartItems.Update(cartItem);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveProductFromCartAsync(Guid productId)
        {
            var cartItem = await _dbContext.CartItems.Include(ci => ci.Products).FirstOrDefaultAsync(ci => ci.Products.Any(p => p.Id == productId));
            if (cartItem == null)
            {
                throw new NotFoundException($"Cart item with product ID {productId} was not found.");
            }

            var productEntity = cartItem.Products.FirstOrDefault(p => p.Id == productId);
            if (productEntity != null)
            {
                cartItem.Products.Remove(productEntity);
                if (!cartItem.Products.Any())
                {
                    _dbContext.CartItems.Remove(cartItem);
                }
                else
                {
                    _dbContext.CartItems.Update(cartItem);
                }
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<decimal> GetTotalCartValueAsync()
        {
            var totalValue = await _dbContext.CartItems
                .Include(ci => ci.Products)
                .SelectMany(ci => ci.Products)
                .SumAsync(p => p.Price);

            return totalValue;
        }

        public async Task<int> GetTotalCartItemsCountAsync()
        {
            var totalCount = await _dbContext.CartItems
                .Include(ci => ci.Products)
                .SelectMany(ci => ci.Products)
                .CountAsync();

            return totalCount;
        }

        public async Task ClearCartAsync()
        {
            var cartItems = await _dbContext.CartItems.ToListAsync();
            _dbContext.CartItems.RemoveRange(cartItems);
            await _dbContext.SaveChangesAsync();
        }
    }
}
