using AppleStore.Core.Models;
using AppleStore.DataAccess.Interfaces;
using AppleStore.ApplicationLayer.Interfaces;
using AppleStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using AppleStore.DataAccess.Repositories;
using Azure;

namespace AppleStore.ApplicationLayer.Services
{
    public class CartItemService : ICartItemServices
    {
        private readonly ICartRepository _cartItemRepository;

        public CartItemService(ICartRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task<List<CartItem>> GetAllCarts()
        {
            return await _cartItemRepository.GetAllCarts();
        }

        public async Task<Guid> CreateCart()
        {
            return await _cartItemRepository.CreateCart();
        }
        public async Task RemoveProduct(Guid cartId, Guid productId)
        {
            await _cartItemRepository.RemoveProduct(cartId, productId);
        }
        public async Task AddProductToCartAsync(Guid cartId, Guid productId)
        {
            await _cartItemRepository.AddProductToCartAsync(cartId, productId);
        }

        public async Task<List<Product>> GetAllProductsInCartAsync(Guid cartId)
        {
            return await _cartItemRepository.GetAllProductsInCartAsync(cartId);
        }

        public async Task<CartItem> GetCartByIdAsync(Guid cartId)
        {
            return await _cartItemRepository.GetCartByIdAsync(cartId);
        }

        public async Task<decimal> GetTotalCartPriceAsync(Guid cartId)
        {
            return await _cartItemRepository.GetTotalCartPriceAsync(cartId);
        }

        public async Task<int> GetTotalProductCountAsync(Guid cartId)
        {
            return await _cartItemRepository.GetTotalProductCountAsync(cartId);
        }

        public async Task SortCartProductsByNameAsync(Guid cartId)
        {
            await _cartItemRepository.SortCartProductsByNameAsync(cartId);
        }

        public async Task SortCartProductsByPriceAsync(Guid cartId)
        {
            await _cartItemRepository.SortCartProductsByPriceAsync(cartId);
        }
    }
}
