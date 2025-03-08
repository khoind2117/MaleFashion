using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Models.DTOs.ProductVariant;

namespace MaleFashion.Server.Services.Interfaces
{
    public interface IProductVariantService
    {
        Task<List<ProductVariantDto>> GetProductVariantsByIdsAsync(List<int> productVariantIds);
        Task<IEnumerable<ProductVariantDto>> GetByProductIdAsync(int productId);
        Task<ProductVariantDto?> GetByIdAsync(int variantId);
        Task AddAsync(ProductVariantRequestDto variantDto);
        Task UpdateAsync(int variantId, ProductVariantRequestDto variantDto);
        Task DeleteAsync(int variantId);
    }
}
