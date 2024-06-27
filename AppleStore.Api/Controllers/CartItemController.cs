using AppleStore.ApplicationLayer.Interfaces;
using AppleStore.DataAccess.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AppleStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                return Ok(carts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("remove-product-idCart{idCart}-idproduct{idProd}")]
        public async Task<IActionResult> RemoveProduct(Guid idCart, Guid idProd)
        {
            try
            {
                await _cartItemServices.RemoveProduct(idCart, idProd);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("create-cart")]
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

        public class Status
        {
            public int Item { get; set; }
        }

        [HttpPost("add-product/{cartId}/{productId}")]
        public async Task<IActionResult> AddProductToCart(Guid cartId, Guid productId)
        {
            var status = new Status();
            try
            {
                await _cartItemServices.AddProductToCartAsync(cartId, productId);
                status.Item = 0;
                return Ok(status);
            }
            catch (RelapseException ex)
            {
                status.Item = 1;
                return Ok(status);
            }
            catch (NullReferenceException ex)
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