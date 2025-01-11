using MaleFashion.Server.Models.DTOs.Account;

namespace MaleFashion.Server.Services.Interfaces
{
    public interface IAccountService
    {
        Task<NewUserDto?> RegisterUserAsync(RegisterDto registerDto);
        Task<NewUserDto?> LoginUserAsync(LoginDto loginDto);
    }
}
