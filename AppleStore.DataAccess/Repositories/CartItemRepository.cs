using AppleStore.Core.Models;
using AppleStore.DataAccess.Entities;
using AppleStore.DataAccess.Exceptions;
using AppleStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace AppleStore.DataAccess.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppleStoreDbContext _dbContext;

        public CartRepository(AppleStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CartItem>> GetAllCarts()
        {
            return await _dbContext.CartItems
                .Select(p => new CartItem(
                    p.Id,
                    p.Products
                        .Select(prod => new Product(
                            prod.Id,
                            prod.Name,
                            prod.Price,
                            prod.Image
                    )).ToList()
                )).ToListAsync();
        }

        public async Task<Guid> CreateCart()
        {
            var cart = new CartItemEntity
            {
                Id = Guid.NewGuid(),
                Products = new List<ProductEntity>()
            };

            _dbContext.CartItems.Add(cart);
            await _dbContext.SaveChangesAsync();
            return cart.Id;

        }

        public async Task<CartItem> GetCartByIdAsync(Guid cartId)
        {
            var cartEntity = await _dbContext.CartItems
                .Include(ci => ci.Products)
                .FirstOrDefaultAsync(ci => ci.Id == cartId);

            if (cartEntity == null)
            {
                throw new NotFoundException($"Cart with ID {cartId} not found.");
            }

            var products = cartEntity.Products.Select(p => new Product
            (
                id: p.Id,
                name: p.Name,
                price: p.Price,
                image: p.Image
            )).ToList();

            return new CartItem
            (
                id: cartEntity.Id,
                products: products
            );
        }

        public async Task AddProductToCartAsync(Guid cartId, Guid productId)
        {
            var cartEntity = await _dbContext.CartItems
                .Include(ci => ci.Products)
                .FirstOrDefaultAsync(ci => ci.Id == cartId);

            if (cartEntity == null)
            {
                throw new NotFoundException($"Cart with ID {cartId} not found.");
            }

           var productEntity = await _dbContext.Products.FindAsync(productId);
            if (productEntity == null)
            {
                throw new NotFoundException($"Cart with ID {productId} not found.");
            }

            //if (cartEntity.Products.Any(p => p.Id == productId))
            productEntity.CartItems.Add(cartEntity);
            cartEntity.Products.Add(productEntity);
            _dbContext.CartItems.Update(cartEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProductsInCartAsync(Guid cartId)
        {
            var cartEntity = await _dbContext.CartItems
                .FirstOrDefaultAsync(ci => ci.Id == cartId);

            if (cartEntity == null)
            {
                throw new NotFoundException($"Cart with ID {cartId} not found.");
            }

            var products = cartEntity.Products.Select(p => new Product
            (
                id: p.Id,
                name: p.Name,
                price: p.Price,
                image: p.Image
            )).ToList();

            return products;
        }

        public async Task<decimal> GetTotalCartPriceAsync(Guid cartId)
        {
            var cartEntity = await _dbContext.CartItems
                .Include(ci => ci.Products)
                .FirstOrDefaultAsync(ci => ci.Id == cartId);

            if (cartEntity == null)
            {
                throw new NotFoundException($"Cart with ID {cartId} not found.");
            }

            return cartEntity.Products.Sum(p => p.Price);
        }

        public async Task<int> GetTotalProductCountAsync(Guid cartId)
        {
            var cartEntity = await _dbContext.CartItems
                .Include(ci => ci.Products)
                .FirstOrDefaultAsync(ci => ci.Id == cartId);

            if (cartEntity == null)
            {
                throw new NotFoundException($"Cart with ID {cartId} not found.");
            }

            return cartEntity.Products.Count;
        }
        public async Task SortCartProductsByPriceAsync(Guid cartId)
        {
            var cartEntity = await _dbContext.CartItems
                .Include(ci => ci.Products)
                .FirstOrDefaultAsync(ci => ci.Id == cartId);

            if (cartEntity == null)
            {
                throw new NotFoundException($"Cart with ID {cartId} not found.");
            }

            cartEntity.Products = cartEntity.Products.OrderBy(p => p.Price).ToList();
            await _dbContext.SaveChangesAsync();
        }

        public async Task SortCartProductsByNameAsync(Guid cartId)
        {
            var cartEntity = await _dbContext.CartItems
                .Include(ci => ci.Products)
                .FirstOrDefaultAsync(ci => ci.Id == cartId);

            if (cartEntity == null)
            {
                throw new NotFoundException($"Cart with ID {cartId} not found.");
            }

            cartEntity.Products = cartEntity.Products.OrderBy(p => p.Name).ToList();
            await _dbContext.SaveChangesAsync();
        }
    }
}