using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Models.DTOs.MainCategory;

namespace MaleFashion.Server.Services.Interfaces
{
    public interface IMainCategoryService
    {
        Task<IEnumerable<MainCategoryDto>> GetAllAsync();
        Task<MainCategoryDto> GetByIdAsync(int id);
        Task AddAsync(MainCategoryRequestDto mainCategoryRequestDto);
        Task UpdateAsync(int id, MainCategoryRequestDto mainCategoryRequestDto);
        Task DeleteAsync(int id);
        Task<PagedDto<MainCategoryDto>> GetPagedAsync(MainCategoryFilterDto mainCategoryFilterDto);
        Task<bool> ExistsAsync(int id);
    }
}
