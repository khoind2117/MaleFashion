using MaleFashion.Server.Models.Entities;

namespace MaleFashion.Server.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
