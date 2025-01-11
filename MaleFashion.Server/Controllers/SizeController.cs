using MaleFashion.Server.Models.DTOs.Size;
using MaleFashion.Server.Services.Implementations;
using MaleFashion.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaleFashion.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var sizes = await _sizeService.GetAllAsync();
                return Ok(sizes);
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
                var size = await _sizeService.GetByIdAsync(id);
                if (size == null)
                {
                    return NotFound();
                }

                return Ok(size);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] SizeRequestDto sizeRequestDto)
        {
            try
            {
                if (sizeRequestDto == null)
                {
                    return BadRequest();
                }

                await _sizeService.AddAsync(sizeRequestDto);
                return Ok(sizeRequestDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] SizeRequestDto sizeRequestDto)
        {
            try
            {
                if (sizeRequestDto == null)
                {
                    return BadRequest();
                }

                var existingSize = await _sizeService.GetByIdAsync(id);
                if (existingSize == null)
                {
                    return NotFound();
                }

                await _sizeService.UpdateAsync(id, sizeRequestDto);
                return Ok();
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
                var existingSize = await _sizeService.GetByIdAsync(id);
                if (existingSize == null)
                {
                    return NotFound();
                }

                await _sizeService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] SizeFilterDto sizeFilterDto)
        {
            try
            {
                var pagedSizes = await _sizeService.GetPagedAsync(sizeFilterDto);
                return Ok(pagedSizes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }
    }
}
