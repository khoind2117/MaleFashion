using MaleFashion.Server.Models.DTOs.Cart;

namespace MaleFashion.Server.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartDto?> GetCartAsync();
        Task<bool> MergeCartAsync();
        Task<bool> AddProductToCartAsync(int productVariantId, int quantity);
        Task<bool> RemoveProductFromCartAsync(int productVariantId);
    }
}
