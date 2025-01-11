using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Models.DTOs.SubCategory;

namespace MaleFashion.Server.Services.Interfaces
{
    public interface ISubCategoryService
    {
        Task<IEnumerable<SubCategoryDto>> GetAllAsync();
        Task<SubCategoryDto> GetByIdAsync(int id);
        Task AddAsync(SubCategoryRequestDto subCategoryRequestDto);
        Task UpdateAsync(int id, SubCategoryRequestDto subCategoryRequestDto);
        Task DeleteAsync(int id);
        Task<PagedDto<SubCategoryDto>> GetPagedAsync(SubCategoryFilterDto subCategoryFilterDto);
        Task<bool> ExistsAsync(int id);
    }
}
