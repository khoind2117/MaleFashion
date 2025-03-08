using MaleFashion.Server.Models.DTOs.Order;
using MaleFashion.Server.Services.Implementations;
using MaleFashion.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MaleFashion.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] OrderFilterDto orderFilterDto)
        {
            try
            {
                var pagedOrders = await _orderService.GetPagedAsync(orderFilterDto);
                return Ok(pagedOrders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetByIdAsync(id);
                if (order == null)
                {
                    return NotFound();
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("check-out")]
        public async Task<IActionResult> CheckOut([FromBody] OrderRequestDto orderRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await _orderService.CheckOutAsync(orderRequestDto);
                return Ok(orderRequestDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            try
            {
                await _orderService.CancelOrderAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "", error = ex.Message });
            }
        }
    }
}
