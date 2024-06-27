using System.Drawing.Printing;
using System.Net.Http;
using System.Text.Json;
using AppleStore.Ui.Models;
using AppleStore.Ui.Interfaces;
using System.Net;
using AppleStore.Ui.Services;
using System.Diagnostics;

namespace AppleStore.Ui.Services
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
    

    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetAllProducts(int page, int pageSize)
        {
            var response = await _httpClient.GetAsync($"/api/Product/products{page}/{pageSize}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Product>>(json);
        }

        public async Task<ApiResponse<List<Product>>> GetProductsAsync(int page, int pageSize)
        {
            var response = await _httpClient.GetAsync($"/api/product/products{page}/{pageSize}");
            var apiResponse = new ApiResponse<List<Product>> { StatusCode = response.StatusCode };
        if (response.IsSuccessStatusCode)
        {
            if (apiResponse.Data == null)  apiResponse.Data = new List<Product>();
            var json = await response.Content.ReadAsStringAsync();
            apiResponse.Data = JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        else
        {
            apiResponse.ErrorMessage = response.ReasonPhrase;
        }
            return apiResponse;
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/product/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Product>(json);
        }
        public class ProductDto
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public byte[] Image { get; set; }
        }
        

        public async Task AddProductAsync(Product product)
        {
            try
            {
                var json = JsonSerializer.Serialize(product);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Product/add-product", content);

                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine($"HTTP Request Exception: {ex.Message}");

                if (ex.StatusCode == HttpStatusCode.BadRequest)
                {
                    Debug.WriteLine($"Server responded with: bad request");
                }
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            var json = JsonSerializer.Serialize(product);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/product/{product.Id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/product/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
