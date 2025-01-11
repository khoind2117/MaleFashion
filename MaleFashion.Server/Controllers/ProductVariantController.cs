using MaleFashion.Server.Models.DTOs.Product;
using MaleFashion.Server.Models.DTOs.ProductVariant;
using MaleFashion.Server.Services.Implementations;
using MaleFashion.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaleFashion.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariantController : ControllerBase
    {
        private readonly IProductVariantService _productVariantService;

        public ProductVariantController(IProductVariantService productVariantService)
        {
            _productVariantService = productVariantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByProductIdAsync(int productId)
        {
            try
            {
                var productVariants = await _productVariantService.GetByProductIdAsync(productId);
                return Ok(productVariants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var productVariant = await _productVariantService.GetByIdAsync(id);
                if (productVariant == null)
                {
                    return NotFound();
                }

                return Ok(productVariant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ProductVariantRequestDto productVariantRequestDto)
        {
            try
            {
                if (productVariantRequestDto == null)
                {
                    return BadRequest();
                }

                await _productVariantService.AddAsync(productVariantRequestDto);
                return Ok(productVariantRequestDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ProductVariantRequestDto productVariantRequestDto)
        {
            try
            {
                if (productVariantRequestDto == null)
                {
                    return BadRequest();
                }

                var existingProductVariant = await _productVariantService.GetByIdAsync(id);
                if (existingProductVariant == null)
                {
                    return NotFound();
                }

                await _productVariantService.UpdateAsync(id, productVariantRequestDto);
                return Ok("Updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [HttpDelete("soft/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var existingProductVariant = await _productVariantService.GetByIdAsync(id);
                if (existingProductVariant == null)
                {
                    return NotFound();
                }

                await _productVariantService.DeleteAsync(id);
                return Ok("Soft Deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }
    }
}
