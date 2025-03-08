using MaleFashion.Server.Models.DTOs.Cart;
using MaleFashion.Server.Models.DTOs.CartItem;
using MaleFashion.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MaleFashion.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetCart()
        {
            try
            {                
                var cart = await _cartService.GetCartAsync();
                if (cart == null)
                {
                    return NotFound();
                }

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("merge")]
        public async Task<IActionResult> MergeCart()
        {
            try
            {
                var result = await _cartService.MergeCartAsync();
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProductToCart([FromBody] CartItemRequestDto cartItemRequestDto)
        {
            try
            {
                var result = await _cartService.AddProductToCartAsync(cartItemRequestDto.ProductVariantId, cartItemRequestDto.Quantity);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("remove/{productVariantId}")]
        public async Task<IActionResult> RemoveProductFromCart(int productVariantId)
        {
            try
            {
                var result = await _cartService.RemoveProductFromCartAsync(productVariantId);

                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
