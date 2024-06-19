using AppleStore.Ui.Dto;
using Newtonsoft.Json;
using System.Net.Http;


namespace AppleStore.Ui.Services
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync();
        Task RemoveProductFromCartAsync(Guid productId);
    }

    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CartDto> GetCartAsync()
        {
            var response = await _httpClient.GetAsync("api/cart");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CartDto>(content);
        }

        public async Task RemoveProductFromCartAsync(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"api/cart/{productId}");
            response.EnsureSuccessStatusCode();
        }
    }
}