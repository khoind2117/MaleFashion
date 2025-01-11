using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Models.DTOs.Size;

namespace MaleFashion.Server.Services.Interfaces
{
    public interface ISizeService
    {
        Task<IEnumerable<SizeDto>> GetAllAsync();
        Task<SizeDto> GetByIdAsync(int id);
        Task AddAsync(SizeRequestDto sizeRequestDto);
        Task UpdateAsync(int id, SizeRequestDto sizeRequestDto);
        Task DeleteAsync(int id);
        Task<PagedDto<SizeDto>> GetPagedAsync(SizeFilterDto sizeFilterDto);
        Task<bool> ExistsAsync(int id);
    }
}
