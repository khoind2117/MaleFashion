using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Services.Implementations;
using MaleFashion.Server.Services.Interfaces;
using MaleFashion.Server.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MaleFashion.Server.Data
{
    public class Seed
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SlugUtil _slugUtil;
        private readonly ITokenService _tokenService;

        public Seed(ApplicationDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SlugUtil slugUtil,
            ITokenService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _slugUtil = slugUtil;
            _tokenService = tokenService;
        }

        public async Task SeedApplicationDbContextAsync()
        {
            #region AspNetRoles
            if (!_context.Roles.Any())
            {
                var roles = new List<IdentityRole>
                {
                    new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                    },
                    new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "User",
                        NormalizedName = "USER",
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                    }
                };

                _context.Roles.AddRange(roles);
                await _context.SaveChangesAsync();
            }
            #endregion

            #region AspNetUsers
            // Seed Admin
            if (!_context.Users.Any())
            {
                var admin = new User
                {
                    UserName = "admin",
                    FirstName = "Hakuren",
                    LastName = "Admin",
                    Address = "",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    NormalizedUserName = "ADMIN",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp= Guid.NewGuid().ToString(),
                    PhoneNumber = "",
                    RefreshToken = _tokenService.GenerateRefreshToken(),
                    RefreshTokenExpiryTime = DateTime.Now.AddDays(7)
                };

                await _userManager.CreateAsync(admin, "Admin@123");
                await _userManager.AddToRoleAsync(admin, "Admin");

                await _context.SaveChangesAsync();
            }
            #endregion
        }
    }
}
