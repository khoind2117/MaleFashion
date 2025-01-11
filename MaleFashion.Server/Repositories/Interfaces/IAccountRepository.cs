using MaleFashion.Server.Models.Entities;

namespace MaleFashion.Server.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> CreateUserAsync(User user, string password);
        Task<User?> FindByUserNameAsync(string username);
        Task<bool> CheckPasswordAsync(User user, string password);
    }
}
