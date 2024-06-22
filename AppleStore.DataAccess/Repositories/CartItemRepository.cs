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

        public async Task RemoveProduct(Guid idCart, Guid idProduct)
        {
            var cart = await _dbContext.CartItems
                .Include(ci => ci.CartItemProducts)
                .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(ci => ci.Id == idCart);

            if (cart == null)
            {
                throw new Exception($"Корзина с Id {idCart} не найдена.");
            }

            var cartItemProduct = cart.CartItemProducts
                .FirstOrDefault(product => product.ProductId == idProduct);

            if (cartItemProduct != null)
            {
                _dbContext.CartItemProducts.Remove(cartItemProduct);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Продукт с Id {idProduct} не найден в корзине.");
            }
        }


        public async Task<List<CartItem>> GetAllCarts()
        {
            try
            {
                var carts = await _dbContext.CartItems
                    .Include(ci => ci.CartItemProducts)
                        .ThenInclude(cp => cp.Product)
                    .ToListAsync();

                var cartItems = carts.Select(cartEntity => new CartItem(
                
                    id: cartEntity.Id,
                    products: cartEntity.CartItemProducts.Select(cp => new Product
                    (
                        id: cp.Product.Id,
                        name: cp.Product.Name,
                        price: cp.Product.Price,
                        image: cp.Product.Image
                    )).ToList()
                )).ToList();

                return cartItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching carts with products: {ex.Message}");
                throw;
            }
        }

        public async Task<Guid> CreateCart()
        {
            try
            {
                var cart = new CartItemEntity();
                _dbContext.CartItems.Add(cart);
                await _dbContext.SaveChangesAsync();
                return cart.Id;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<CartItem> GetCartByIdAsync(Guid cartId)
        {
            var cartEntity = await _dbContext.CartItems
                .Include(ci => ci.CartItemProducts)
                    .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(ci => ci.Id == cartId);

            if (cartEntity == null)
            {
                throw new NotFoundException($"Cart with ID {cartId} not found.");
            }

            var products = cartEntity.CartItemProducts
                .Select(cp => new Product
                (
                    id: cp.Product.Id,
                    name: cp.Product.Name,
                    price: cp.Product.Price,
                    image: cp.Product.Image
                ))
                .ToList();

            var cartItem = new CartItem
            (
                id: cartEntity.Id,
                products: products
            );

            return cartItem;
        }
        public async Task AddProductToCartAsync(Guid cartId, Guid productId)
        {
            try
            {
                var cart = await _dbContext.CartItems
                    .Include(ci => ci.CartItemProducts)
                        .ThenInclude(cp => cp.Product)
                    .FirstOrDefaultAsync(ci => ci.Id == cartId);

                if (cart == null)
                {
                    throw new NullReferenceException($"Корзина с Id {cartId} не найдена.");
                }

                var product = await _dbContext.Products
                    .FirstOrDefaultAsync(p => p.Id == productId);

                if (product == null)
                {
                    throw new NullReferenceException($"Продукт с Id {productId} не найден.");
                }

                if (cart.CartItemProducts.Any(cp => cp.ProductId == productId))
                {
                    throw new RelapseException($"Продукт с Id {productId} уже находится в корзине.");
                }

                var cartItemProduct = new CartItemProductEntity
                {
                    CartItemId = cartId,
                    ProductId = productId
                };

                _dbContext.CartItemProducts.Add(cartItemProduct);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Произошла ошибка при сохранении изменений в базе данных.");
            }
        }

        public async Task<List<Product>> GetAllProductsInCartAsync(Guid cartId)
        {
            var cartEntity = await _dbContext.CartItems
                .FirstOrDefaultAsync(ci => ci.Id == cartId);

            if (cartEntity == null)
            {
                throw new NotFoundException($"Cart with ID {cartId} not found.");
            }

            var products = cartEntity.CartItemProducts.Select(p => new Product
            (
                id: p.Product.Id,
                name: p.Product.Name,
                price: p.Product.Price,
                image: p.Product.Image
            )).ToList();

            return products;
        }

        public async Task<decimal> GetTotalCartPriceAsync(Guid cartId)
        {
            var cartEntity = await _dbContext.CartItems
                .Include(ci => ci.CartItemProducts)
                    .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(ci => ci.Id == cartId);

            if (cartEntity == null)
            {
                throw new NotFoundException($"Cart with ID {cartId} not found.");
            }

            return 0;
        }

        public async Task<int> GetTotalProductCountAsync(Guid cartId)
        {
            var cartEntity = await _dbContext.CartItems
                .Include(ci => ci.CartItemProducts)
                    .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(ci => ci.Id == cartId);

            if (cartEntity == null)
            {
                throw new NotFoundException($"Cart with ID {cartId} not found.");
            }

            return 0;
        }
        public async Task SortCartProductsByPriceAsync(Guid cartId)
        {
            try
            {
                var cart = await _dbContext.CartItems
                    .Include(ci => ci.CartItemProducts)
                    .ThenInclude(cp => cp.Product)
                    .FirstOrDefaultAsync(ci => ci.Id == cartId);

                if (cart == null)
                {
                    throw new Exception($"Корзина с Id {cartId} не найдена.");
                }

                var products = cart.CartItemProducts
                    .Select(cp => cp.Product) 
                    .OrderBy(p => p.Price)    
                    .ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving products from cart: {ex.Message}");
                throw;
            }
        }

        public async Task SortCartProductsByNameAsync(Guid cartId)
        {
            try
            {
                var cartEntity = await _dbContext.CartItems
                    .Include(ci => ci.CartItemProducts)
                        .ThenInclude(cp => cp.Product)
                    .FirstOrDefaultAsync(ci => ci.Id == cartId);

                if (cartEntity == null)
                {
                    throw new NotFoundException($"Cart with ID {cartId} not found.");
                }

                cartEntity.CartItemProducts = cartEntity.CartItemProducts
                    .OrderBy(cp => cp.Product.Name)
                    .ToList();

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sorting cart products by name: {ex.Message}");
                throw;
            }
        }
    }
}