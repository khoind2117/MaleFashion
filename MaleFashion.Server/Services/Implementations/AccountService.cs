using MaleFashion.Server.Models.DTOs.Account;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Repositories.Interfaces;
using MaleFashion.Server.Services.Interfaces;

namespace MaleFashion.Server.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenService _tokenService;

        public AccountService(IAccountRepository accountRepository, ITokenService tokenService)
        {
            _accountRepository = accountRepository;
            _tokenService = tokenService;
        }

        private NewUserDto CreateNewUserDto(User user)
        {
            return new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }

        public async Task<NewUserDto?> RegisterUserAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                UserName = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Address = registerDto.Address,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
            };

            var result = await _accountRepository.CreateUserAsync(user, registerDto.Password);

            return result ? CreateNewUserDto(user) : null;
        }

        public async Task<NewUserDto?> LoginUserAsync(LoginDto loginDto)
        {
            var user = await _accountRepository.FindByUserNameAsync(loginDto.UserName);

            if (user == null)
            {
                return null;
            }

            var isPasswordValid = await _accountRepository.CheckPasswordAsync(user, loginDto.Password);

            return isPasswordValid ? CreateNewUserDto(user) : null;
        }
    }
}
