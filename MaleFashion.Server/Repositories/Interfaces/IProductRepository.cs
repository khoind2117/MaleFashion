using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Models.DTOs.Product;
using MaleFashion.Server.Models.Entities;

namespace MaleFashion.Server.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product?> GetProductById(int id);
        Task<PagedDto<PagedProductDto>> GetPagedProductsAsync(ProductFilterDto productFilterDto);
    }
}
