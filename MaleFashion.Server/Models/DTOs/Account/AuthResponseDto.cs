namespace MaleFashion.Server.Models.DTOs.Account
{
    public class AuthResponseDto
    {
        public required string UserId { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public List<string>? Roles { get; set; }
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
    }
}
