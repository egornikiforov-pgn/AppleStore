using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using AppleStore.Ui.Dto;

namespace AppleStore.Ui.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task AddProductToCartAsync(ProductDto product);
    }

    public class ProductService : IProductService
    {
       
            private readonly HttpClient _httpClient;

            public ProductService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
            {
                var response = await _httpClient.GetAsync("api/products");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(content);
            }

            public async Task AddProductToCartAsync(ProductDto product)
            {
                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/cart", content);
                response.EnsureSuccessStatusCode();
            }
        
    }
}
