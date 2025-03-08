using MaleFashion.Server.Models.DTOs.Account;
using MaleFashion.Server.Models.DTOs.User;
using System.Security.Claims;

namespace MaleFashion.Server.Services.Interfaces
{
    public interface IAccountService
    {
        Task<UserDto?> GetCurrentUser(ClaimsPrincipal user);
        Task<AuthResponseDto?> RegisterUserAsync(RegisterDto registerDto);
        Task<AuthResponseDto?> LoginUserAsync(LoginDto loginDto, HttpResponse response);
        Task LogoutAsync(HttpRequest request, HttpResponse response);
    }
}
