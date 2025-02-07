using MaleFashion.Server.Models.DTOs.Account;

namespace MaleFashion.Server.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AuthResponseDto?> RegisterUserAsync(RegisterDto registerDto);
        Task<AuthResponseDto?> LoginUserAsync(LoginDto loginDto, HttpResponse response);
        Task LogoutAsync(HttpRequest request, HttpResponse response);
    }
}
