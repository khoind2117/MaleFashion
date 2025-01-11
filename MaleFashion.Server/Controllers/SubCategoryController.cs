using MaleFashion.Server.Models.DTOs.SubCategory;
using MaleFashion.Server.Services.Implementations;
using MaleFashion.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaleFashion.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var subCategories = await _subCategoryService.GetAllAsync();
                return Ok(subCategories);
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
                var subCategory = await _subCategoryService.GetByIdAsync(id);
                if (subCategory == null)
                {
                    return NotFound();
                }

                return Ok(subCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] SubCategoryRequestDto subCategoryRequestDto)
        {
            try
            {
                if (subCategoryRequestDto == null)
                {
                    return BadRequest();
                }

                await _subCategoryService.AddAsync(subCategoryRequestDto);
                return Ok(subCategoryRequestDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] SubCategoryRequestDto subCategoryRequestDto)
        {
            try
            {
                if (subCategoryRequestDto == null)
                {
                    return BadRequest();
                }

                var existingSubCategory = await _subCategoryService.GetByIdAsync(id);
                if (existingSubCategory == null)
                {
                    return NotFound();
                }

                await _subCategoryService.UpdateAsync(id, subCategoryRequestDto);
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
                var existingSubCategory = await _subCategoryService.GetByIdAsync(id);
                if (existingSubCategory == null)
                {
                    return NotFound();
                }

                await _subCategoryService.DeleteAsync(id);
                return Ok("Deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] SubCategoryFilterDto subCategoryFilterDto)
        {
            try
            {
                var pagedSubCategories = await _subCategoryService.GetPagedAsync(subCategoryFilterDto);
                return Ok(pagedSubCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }
    }
}
