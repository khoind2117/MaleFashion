using MaleFashion.Server.Models.DTOs.Size;
using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Repositories.Interfaces;
using MaleFashion.Server.Services.Interfaces;
using MaleFashion.Server.Models.Entities;

namespace MaleFashion.Server.Services.Implementations
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;

        public SizeService(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }

        public async Task AddAsync(SizeRequestDto sizeRequestDto)
        {
            var size = new Size
            {
                Name = sizeRequestDto.Name,
            };

            await _sizeRepository.AddAsync(size);
        }

        public async Task DeleteAsync(int id)
        {
            var size = await _sizeRepository.GetByIdAsync(id);
            if (size == null)
            {
                throw new KeyNotFoundException();
            }

            await _sizeRepository.DeleteAsync(size);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var size = await _sizeRepository.GetByIdAsync(id);
            return size != null;
        }

        public async Task<IEnumerable<SizeDto>> GetAllAsync()
        {
            var sizes = await _sizeRepository.GetAllAsync();

            var sizeDtos = sizes.Select(size => new SizeDto
            {
                Id = size.Id,
                Name = size.Name
            });

            return sizeDtos;
        }

        public async Task<SizeDto> GetByIdAsync(int id)
        {
            var size = await _sizeRepository.GetByIdAsync(id);
            if (size == null)
            {
                throw new KeyNotFoundException();
            }

            var sizeDto = new SizeDto
            {
                Id = size.Id,
                Name = size.Name
            };

            return sizeDto;
        }

        public async Task<PagedDto<SizeDto>> GetPagedAsync(SizeFilterDto sizeFilterDto)
        {
            var query = await _sizeRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(sizeFilterDto.Keyword))
            {
                query = query.Where(p => p.Name.Contains(sizeFilterDto.Keyword));
            }

            if (!string.IsNullOrEmpty(sizeFilterDto.OrderBy))
            {
                switch (sizeFilterDto.OrderBy.ToLower())
                {
                    case "name":
                        query = sizeFilterDto.IsDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                        break;
                    default:
                        break;
                }
            }

            var totalRecords = query.Count();
            var pagedProducts = query.Skip(sizeFilterDto.GetSkip())
                                    .Take(sizeFilterDto.GetTake());

            IEnumerable<SizeDto> sizeDtos = query.Select(p => new SizeDto
            {
                Id = p.Id,
                Name = p.Name,
            }).ToList();

            return new PagedDto<SizeDto>(totalRecords, sizeDtos.ToList());
        }

        public async Task UpdateAsync(int id, SizeRequestDto sizeRequestDto)
        {
            var size = await _sizeRepository.GetByIdAsync(id);
            if (size == null)
            {
                throw new KeyNotFoundException();
            }

            size.Name = sizeRequestDto.Name;

            await _sizeRepository.UpdateAsync(size);
        }
    }
}
