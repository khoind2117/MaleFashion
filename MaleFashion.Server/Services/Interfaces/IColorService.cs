using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Models.DTOs.Color;

namespace MaleFashion.Server.Services.Interfaces
{
    public interface IColorService
    {
        Task<IEnumerable<ColorDto>> GetAllAsync();
        Task<ColorDto> GetByIdAsync(int id);
        Task AddAsync(ColorRequestDto colorRequestDto);
        Task UpdateAsync(int id, ColorRequestDto colorRequestDto);
        Task DeleteAsync(int id);
        Task<PagedDto<ColorDto>> GetPagedAsync(ColorFilterDto colorFilterDto);
        Task<bool> ExistsAsync(int id);
    }
}
