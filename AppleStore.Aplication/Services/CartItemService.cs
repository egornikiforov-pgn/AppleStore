using AppleStore.Core.Models;
using AppleStore.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppleStore.Application.Services
{
    public class CartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;

        public CartItemService(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task<List<CartItem>> GetCartItemsAsync()
        {
            return await _cartItemRepository.GetCartItemsAsync();
        }

        public async Task AddProductToCartAsync(Guid productId)
        {
            await _cartItemRepository.AddProductToCartAsync(productId);
        }

        public async Task RemoveProductFromCartAsync(Guid productId)
        {
            await _cartItemRepository.RemoveProductFromCartAsync(productId);
        }

        public async Task<decimal> GetTotalCartValueAsync()
        {
            return await _cartItemRepository.GetTotalCartValueAsync();
        }

        public async Task<int> GetTotalCartItemsCountAsync()
        {
            return await _cartItemRepository.GetTotalCartItemsCountAsync();
        }

        public async Task ClearCartAsync()
        {
            await _cartItemRepository.ClearCartAsync();
        }
    }
}
