using Azure;
using MaleFashion.Server.Models.DTOs.Account;
using MaleFashion.Server.Models.DTOs.User;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Repositories.Interfaces;
using MaleFashion.Server.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MaleFashion.Server.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;

        public AccountService(IUnitOfWork unitOfWork,
            ITokenService tokenService,
            UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        private async Task<AuthResponseDto> CreateAuthResponseDtoAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _tokenService.CreateAccessToken(user, roles.ToList());

            return new AuthResponseDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles.ToList(),
                AccessToken = accessToken,
            };
        }

        public async Task<UserDto?> GetCurrentUser(ClaimsPrincipal userPrincipal)
        {
            var userId = userPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("Invalid token");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
            };

            return userDto;
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

            var result = await _unitOfWork.AccountRepository.CreateUserAsync(user, registerDto.Password);
            if (!result) return null;

            await _userManager.AddToRoleAsync(user, "User");


            // Create User Cart After Successfully Register
            var cart = new Cart { UserId = user.Id };
            await _unitOfWork.CartRepository.AddAsync(cart);
            await _unitOfWork.SaveChangesAsync();

            return await CreateAuthResponseDtoAsync(user);
        }

        public async Task<AuthResponseDto?> LoginUserAsync(LoginDto loginDto, HttpResponse response)
        {
            var user = await _unitOfWork.AccountRepository.FindByUserNameAsync(loginDto.UserName);
            if (user == null) return null;

            var isPasswordValid = await _unitOfWork.AccountRepository.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid) return null;

            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(10);
            await _userManager.UpdateAsync(user);

            response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
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
            response.Cookies.Delete("BasketId");
        }
    }
}
