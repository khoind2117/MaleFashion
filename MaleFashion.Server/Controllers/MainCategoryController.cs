using MaleFashion.Server.Models.DTOs.MainCategory;
using MaleFashion.Server.Services.Implementations;
using MaleFashion.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaleFashion.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainCategoryController : ControllerBase
    {
        private readonly IMainCategoryService _mainCategoryService;

        public MainCategoryController(IMainCategoryService mainCategoryService)
        {
            _mainCategoryService = mainCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var mainCategories = await _mainCategoryService.GetAllAsync();
                return Ok(mainCategories);
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
                var mainCategory = await _mainCategoryService.GetByIdAsync(id);
                if (mainCategory == null)
                {
                    return NotFound();
                }

                return Ok(mainCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] MainCategoryRequestDto mainCategoryRequestDto)
        {
            try
            {
                if (mainCategoryRequestDto == null)
                {
                    return BadRequest();
                }

                await _mainCategoryService.AddAsync(mainCategoryRequestDto);
                return Ok(mainCategoryRequestDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] MainCategoryRequestDto mainCategoryRequestDto)
        {
            try
            {
                if (mainCategoryRequestDto == null)
                {
                    return BadRequest();
                }

                var existingMainCategory = await _mainCategoryService.GetByIdAsync(id);
                if (existingMainCategory == null)
                {
                    return NotFound();
                }

                await _mainCategoryService.UpdateAsync(id, mainCategoryRequestDto);
                return Ok("Updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var existingMainCategory = await _mainCategoryService.GetByIdAsync(id);
                if (existingMainCategory == null)
                {
                    return NotFound();
                }

                await _mainCategoryService.DeleteAsync(id);
                return Ok("Deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] MainCategoryFilterDto mainCategoryFilterDto)
        {
            try
            {
                var pagedMainCategories = await _mainCategoryService.GetPagedAsync(mainCategoryFilterDto);
                return Ok(pagedMainCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }
    }
}
