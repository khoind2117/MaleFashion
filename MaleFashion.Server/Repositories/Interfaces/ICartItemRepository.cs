using MaleFashion.Server.Models.Entities;

namespace MaleFashion.Server.Repositories.Interfaces
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        //Task<List<CartItem>> GetCartItemsByCartIdAsync(int cartId);
        Task ClearCartItemsAsync(int cartId);
    }
}
