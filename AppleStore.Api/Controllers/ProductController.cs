using AppleStore.DataAccess.Exceptions;
using AppleStore.Core.Models;
using AppleStore.ApplicationLayer.Services;
using AppleStore.ApplicationLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace AppleStore.Api.Controllers
{
    public class ProductWithImageDto
    {   
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
            [HttpGet("products")]
            public async Task<ActionResult<List<Product>>> GetProducts(int page = 1, int pageSize = 20)
            {
                var products = await _productService.GetProductsAsync(page, pageSize);
                return Ok(products);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Product>> GetProductById(Guid id)
            {
                try
                {
                    var product = await _productService.GetProductByIdAsync(id);
                    return Ok(product);
                }
                catch (NotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
            }

            [HttpPost("add-product")]
            public async Task<IActionResult> AddProduct([FromForm] ProductWithImageDto productDto)
            {
            var product = new Product(
                    id: Guid.NewGuid(),
                    name: productDto.Name,
                    price: productDto.Price,
                    image: await GetByteArraFromImage(productDto.Image)
                );
                try
                {
                    await _productService.AddProductAsync(product);
                return Ok();
                }
                catch (ProductIsNullExceptoin ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateProduct(Guid id, Product product)
            {
                if (id != product.Id)
                {
                    return BadRequest("Product ID mismatch.");
                }

                try
                {
                    await _productService.UpdateProductAsync(product);
                    return NoContent();
                }
                catch (NotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (ProductIsNullExceptoin ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteProduct(Guid id)
            {
                try
                {
                    await _productService.DeleteProductAsync(id);
                    return NoContent();
                }
                catch (NotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
            }
        private async Task<byte[]> GetByteArraFromImage(IFormFile image)
        {
            using (var ms = new MemoryStream())
            {
                await image.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}
