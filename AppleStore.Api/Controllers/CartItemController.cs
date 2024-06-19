using AppleStore.ApplicationLayer.Interfaces;
using AppleStore.Core.Models;
using AppleStore.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppleStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartItemServices _cartItemServices;

        public CartController(ICartItemServices cartServices)
        {
            _cartItemServices = cartServices;
        }

        [HttpPost("get-all-cart")]
        public async Task<IActionResult> GetAllCart()
        {
            try
            {
                var carts = await _cartItemServices.GetAllCarts();
                return Ok(new { carts});
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("create-cart")]
        public async Task<IActionResult> CreateCart()
        {
            try
            {
                var id = await _cartItemServices.CreateCart();
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add-product/{cartId}/{productId}")]
        public async Task<IActionResult> AddProductToCart(Guid cartId, Guid productId)
        {
            try
            {
                await _cartItemServices.AddProductToCartAsync(cartId, productId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-cart/{cartId}")]
        public async Task<IActionResult> GetCart(Guid cartId)
        {
            try
            {
                var cart = await _cartItemServices.GetCartByIdAsync(cartId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-all-products/{cartId}")]
        public async Task<IActionResult> GetAllProductsInCart(Guid cartId)
        {
            try
            {
                var products = await _cartItemServices.GetAllProductsInCartAsync(cartId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-total-price/{cartId}")]
        public async Task<IActionResult> GetTotalCartPrice(Guid cartId)
        {
            try
            {
                var totalPrice = await _cartItemServices.GetTotalCartPriceAsync(cartId);
                return Ok(totalPrice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-total-product-count/{cartId}")]
        public async Task<IActionResult> GetTotalProductCount(Guid cartId)
        {
            try
            {
                var totalCount = await _cartItemServices.GetTotalProductCountAsync(cartId);
                return Ok(totalCount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("sort-by-price/{cartId}")]
        public async Task<IActionResult> SortProductsByPrice(Guid cartId)
        {
            try
            {
                await _cartItemServices.SortCartProductsByPriceAsync(cartId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("sort-by-name/{cartId}")]
        public async Task<IActionResult> SortProductsByName(Guid cartId)
        {
            try
            {
                await _cartItemServices.SortCartProductsByNameAsync(cartId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}