using MaleFashion.Server.Data;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MaleFashion.Server.Repositories.Implementations
{
    public class CartItemRepository : GenericRepository<ApplicationDbContext, CartItem>, ICartItemRepository
    {
        public CartItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<CartItem>> GetCartItemsByCartIdAsync(int cartId)
        {
            var cartItems = await _dbSet.Where(ci => ci.CartId == cartId)
                                    .Include(ci => ci.ProductVariant)
                                    .ToListAsync();

            return cartItems;
        }

        public async Task ClearCartItemsAsync(int cartId)
        {
            var cartItems = await _dbSet.Where(ci => ci.CartId == cartId)
                                    .ToListAsync(); ;

            _dbSet.RemoveRange(cartItems);

            await _context.SaveChangesAsync();
        }
    }
}
