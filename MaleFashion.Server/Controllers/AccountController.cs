using MaleFashion.Server.Models.DTOs.Account;
using MaleFashion.Server.Services.Implementations;
using MaleFashion.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaleFashion.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService,
            ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var userDto = await _accountService.GetCurrentUser(User);
                return Ok(userDto);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Invalid token" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
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
                    return Ok(newUserDto);
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
                var authResult = await _accountService.LoginUserAsync(loginDto, Response);
                if (authResult != null)
                {
                    return Ok(authResult);
                }

                return BadRequest(new { message = "Incorrect username or password." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _accountService.LogoutAsync(Request, Response);
                return Ok(new { message = "Logged out successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                return Unauthorized("No refresh token provided");
            }

            var newAccessToken = await _tokenService.RefreshAccessTokenAsync(refreshToken, Response);

            if (newAccessToken == null)
            {
                return Unauthorized("Invalid or expired refresh token");
            }

            return Ok(new { accessToken = newAccessToken });
        }

        [Authorize]
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken()
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                return Unauthorized("No refresh token provided");
            }

            var isRevoked = await _tokenService.RevokeRefreshTokenAsync(refreshToken, Response);

            if (!isRevoked)
            {
                return Unauthorized("Invalid refresh token or token already revoked");
            }

            return Ok("Refresh token revoked successfully");
        }
    }

}
