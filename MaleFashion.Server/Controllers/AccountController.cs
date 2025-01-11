using MaleFashion.Server.Models.DTOs.Account;
using MaleFashion.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MaleFashion.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newUserDto = await _accountService.RegisterUserAsync(registerDto);

                if (newUserDto != null)
                {
                    return CreatedAtAction(nameof(Register), new { username = newUserDto.UserName }, newUserDto);
                }
                return StatusCode(500, "Failed to create user.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var authResult = await _accountService.LoginUserAsync(loginDto);

                if (authResult != null)
                {
                    return Ok(authResult);
                }
                return Unauthorized("Incorrect username or password.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }

}
