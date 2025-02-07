using Azure;
using MaleFashion.Server.Models.DTOs.Account;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Repositories.Interfaces;
using MaleFashion.Server.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MaleFashion.Server.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;

        public AccountService(IAccountRepository accountRepository,
            ITokenService tokenService,
            UserManager<User> userManager)
        {
            _accountRepository = accountRepository;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        private async Task<AuthResponseDto> CreateAuthResponseDtoAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponseDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles.ToList(),
                Token = _tokenService.CreateAccessToken(user, roles.ToList()),
                RefreshToken = user.RefreshToken,
            };
        }

        public async Task<AuthResponseDto?> RegisterUserAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                UserName = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Address = registerDto.Address,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                RefreshToken = _tokenService.GenerateRefreshToken(),
                RefreshTokenExpiryTime = DateTime.Now.AddDays(10),
            };

            var result = await _accountRepository.CreateUserAsync(user, registerDto.Password);
            if (!result) return null;

            await _userManager.AddToRoleAsync(user, "User");

            return await CreateAuthResponseDtoAsync(user);
        }

        public async Task<AuthResponseDto?> LoginUserAsync(LoginDto loginDto, HttpResponse response)
        {
            var user = await _accountRepository.FindByUserNameAsync(loginDto.UserName);
            if (user == null) return null;

            var isPasswordValid = await _accountRepository.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid) return null;

            response.Cookies.Append("refreshToken", user.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = user.RefreshTokenExpiryTime
            });

            return await CreateAuthResponseDtoAsync(user);
        }

        public async Task LogoutAsync(HttpRequest request, HttpResponse response)
        {
            if (!request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                return;
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = DateTime.MinValue;
                await _userManager.UpdateAsync(user);
            }

            response.Cookies.Delete("refreshToken");
        }
    }
}
