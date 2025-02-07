using MaleFashion.Server.Models.Entities;
using System.Security.Claims;

namespace MaleFashion.Server.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateAccessToken(User user, List<string> roles);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
        Task<string?> RefreshAccessTokenAsync(string refreshToken, HttpResponse response);
        Task<bool> RevokeRefreshTokenAsync(string refreshToken, HttpResponse response);
    }
}
