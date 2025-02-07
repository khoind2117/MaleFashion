using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Models.DTOs.Product;

namespace MaleFashion.Server.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDetailDto> GetByIdAsync(int id);
        Task AddAsync(ProductRequestDto productRequestDto);
        Task UpdateAsync(int id, ProductRequestDto productRequestDto);
        Task SoftDeleteAsync(int id);
        Task HardDeleteAsync(int id);
        Task<PagedDto<PagedProductDto>> GetPagedAsync(ProductFilterDto productFilterDto);
        Task<bool> ExistsAsync(int id);
    }
}
