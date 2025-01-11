using MaleFashion.Server.Models.DTOs.Color;
using MaleFashion.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MaleFashion.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var colors = await _colorService.GetAllAsync();
                return Ok(colors);
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
                var color = await _colorService.GetByIdAsync(id);
                if (color == null)
                {
                    return NotFound();
                }

                return Ok(color);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ColorRequestDto colorRequestDto)
        {
            try
            {
                if (colorRequestDto == null)
                {
                    return BadRequest();
                }

                await _colorService.AddAsync(colorRequestDto);
                return Ok(colorRequestDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ColorRequestDto colorRequestDto)
        {
            try
            {
                if (colorRequestDto == null)
                {
                    return BadRequest("Color data is incorrect.");
                }

                var existingColor = await _colorService.GetByIdAsync(id);
                if (existingColor == null)
                {
                    return NotFound();
                }

                await _colorService.UpdateAsync(id, colorRequestDto);
                return Ok("Color updated successfully.");
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
                var existingColor = await _colorService.GetByIdAsync(id);
                if (existingColor == null)
                {
                    return NotFound();
                }

                await _colorService.DeleteAsync(id);
                return Ok("Color deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] ColorFilterDto colorFilterDto)
        {
            try
            {
                var pagedColors = await _colorService.GetPagedAsync(colorFilterDto);
                return Ok(pagedColors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }
    }
}
