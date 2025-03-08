using MaleFashion.Server.Models.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace MaleFashion.Server.Repositories.Interfaces
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<Cart?> GetCartByUserIdAsync(string userId);
        Task<Cart?> GetCartByBasketIdAsync(Guid basketId);
    }
}
