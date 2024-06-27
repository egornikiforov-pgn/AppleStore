using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AppleStore.Ui.Models;
using AppleStore.Ui.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace AppleStore.Ui.Services
{

    public class CartResponse
    {
        public Guid Id { get; set; }
        public List<Product> Products { get; set; }
    }
    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CartItem> GetCart(Guid cartId)
        {
            var response = await _httpClient.PostAsync($"get-cart/{cartId}", null);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CartItem>(json);
        }

        public async Task<List<CartItem>> GetAllCart()
        {
            var response = await _httpClient.PostAsync("get-all-cart", null);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<CartItem>>(json);
        }
        public async Task<Guid> CreateCartAsync()
        {
            var response = await _httpClient.GetAsync("/api/Cart/create-cart");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Guid>(json);
        }

        public class Status
        { 
            public int Item { get; set; }
        }

        public async Task<Status> AddProductToCartAsync(Guid cartId, Guid productId)
        {
            var response = await _httpClient.PostAsync($"api/cart/add-product/{cartId}/{productId}", null);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Status>(json);
        }
       
        public async Task<ApiResponse<List<Product>>> GetAllProductsInCartAsync(Guid cartId)
        {
            var response = await _httpClient.GetAsync($"/api/Cart/get-cart/{cartId}");
            var apiResponse = new ApiResponse<List<Product>> { StatusCode = response.StatusCode };

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var temp = JsonSerializer.Deserialize<CartResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                apiResponse.Data = temp.Products;
            }
            else
            {
                apiResponse.ErrorMessage = response.ReasonPhrase;
            }
                return apiResponse;
        }
        public async Task<decimal> GetTotalCartPriceAsync(Guid cartId)
        {
            var response = await _httpClient.GetAsync($"api/cart/get-total-price/{cartId}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<decimal>(json);
        }

        public async Task<int> GetTotalProductCountAsync(Guid cartId)
        {
            var response = await _httpClient.GetAsync($"api/cart/get-total-product-count/{cartId}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<int>(json);
        }

        public async Task RemoveProductFromCartAsync(Guid cartId, Guid productId)
        {
            var response = await _httpClient.PostAsync($"/api/Cart/remove-product-idCart{cartId}-idproduct{productId}",null);
            response.EnsureSuccessStatusCode();
        }
    }
}