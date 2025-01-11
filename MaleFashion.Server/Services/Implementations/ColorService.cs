using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Models.DTOs.Color;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Repositories.Implementations;
using MaleFashion.Server.Repositories.Interfaces;
using MaleFashion.Server.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace MaleFashion.Server.Services.Implementations
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;

        public ColorService(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        public async Task<IEnumerable<ColorDto>> GetAllAsync()
        {
            var colors = await _colorRepository.GetAllAsync();

            var colorDtos = colors.Select(color => new ColorDto
            {
                Id = color.Id,
                Name = color.Name,
                ColorCode = color.ColorCode
            });

            return colorDtos;
        }

        public async Task<ColorDto> GetByIdAsync(int id)
        {
            var color = await _colorRepository.GetByIdAsync(id);
            if (color == null)
            {
                throw new KeyNotFoundException("Color not found");
            }

            var colorDto = new ColorDto
            {
                Id = color.Id,
                Name = color.Name,
                ColorCode = color.ColorCode
            };

            return colorDto;
        }

        public async Task AddAsync(ColorRequestDto colorRequestDto)
        {
            var color = new Color
            {
                Name = colorRequestDto.Name,
                ColorCode = colorRequestDto.ColorCode
            };

            await _colorRepository.AddAsync(color);
        }

        public async Task UpdateAsync(int id, ColorRequestDto colorRequestDto)
        {
            var color = await _colorRepository.GetByIdAsync(id);
            if (color == null)
            {
                throw new KeyNotFoundException("Color not found");
            }

            color.Name = colorRequestDto.Name;
            color.ColorCode = colorRequestDto.ColorCode;

            await _colorRepository.UpdateAsync(color);
        }

        public async Task DeleteAsync(int id)
        {
            var color = await _colorRepository.GetByIdAsync(id);
            if (color == null)
            {
                throw new KeyNotFoundException("Color not found");
            }

            await _colorRepository.DeleteAsync(color);
        }

        public async Task<PagedDto<ColorDto>> GetPagedAsync(ColorFilterDto colorFilterDto)
        {
            var query = await _colorRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(colorFilterDto.Keyword))
            {
                query = query.Where(p => p.Name.Contains(colorFilterDto.Keyword));
            }

            if (!string.IsNullOrEmpty(colorFilterDto.OrderBy))
            {
                switch (colorFilterDto.OrderBy.ToLower())
                {
                    case "name":
                        query = colorFilterDto.IsDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                        break;
                    default:
                        break;
                }
            }

            var totalRecords = query.Count();
            var pagedProducts = query.Skip(colorFilterDto.GetSkip())
                                    .Take(colorFilterDto.GetTake());

            IEnumerable<ColorDto> colorDtos = query.Select(p => new ColorDto
            {
                Id = p.Id,
                Name = p.Name,
                ColorCode = p.ColorCode
            }).ToList();

            return new PagedDto<ColorDto>(totalRecords, colorDtos.ToList());
        }


        public async Task<bool> ExistsAsync(int id)
        {
            var color = await _colorRepository.GetByIdAsync(id);
            return color != null;
        }
    }
}
