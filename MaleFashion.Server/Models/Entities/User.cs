using Microsoft.AspNetCore.Identity;

namespace MaleFashion.Server.Models.Entities
{
    public class User : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Address { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public virtual Cart? Cart { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }

        public virtual ICollection<Favorite>? Favorites { get; set; }
    }
}
