using MaleFashion.Server.Models.Entities;

namespace MaleFashion.Server.Repositories.Interfaces
{
    public interface IProductVariantRepository : IGenericRepository<ProductVariant>
    {
        Task<IEnumerable<ProductVariant>> GetByProductIdAsync(int productId);
        Task<List<ProductVariant>> GetProductVariantsByIdsAsync(List<int> productVariantIds);
    }
}
