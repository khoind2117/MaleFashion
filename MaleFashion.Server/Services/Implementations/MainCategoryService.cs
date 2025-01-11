using MaleFashion.Server.Models.DTOs.Color;
using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Repositories.Interfaces;
using MaleFashion.Server.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Models.DTOs.MainCategory;
using MaleFashion.Server.Utilities;

namespace MaleFashion.Server.Services.Implementations
{
    public class MainCategoryService : IMainCategoryService
    {
        private readonly IMainCategoryRepository _mainCategoryRepository;
        private readonly SlugUtil _slugUtil;

        public MainCategoryService(IMainCategoryRepository mainCategoryRepository,
            SlugUtil slugUtil)
        {
            _mainCategoryRepository = mainCategoryRepository;
            _slugUtil = slugUtil;
        }

        public async Task<IEnumerable<MainCategoryDto>> GetAllAsync()
        {
            var mainCategories = await _mainCategoryRepository.GetAllAsync();

            var mainCategoryDtos = mainCategories.Select(mainCateogry => new MainCategoryDto
            {
                Id = mainCateogry.Id,
                Name = mainCateogry.Name,
                Slug = mainCateogry.Slug
            });

            return mainCategoryDtos;
        }

        public async Task<MainCategoryDto> GetByIdAsync(int id)
        {
            var mainCategory = await _mainCategoryRepository.GetByIdAsync(id);
            if (mainCategory == null)
            {
                throw new KeyNotFoundException();
            }

            var mainCategoryDto = new MainCategoryDto
            {
                Id = mainCategory.Id,
                Name = mainCategory.Name,
                Slug = mainCategory.Slug
            };

            return mainCategoryDto;
        }

        public async Task AddAsync(MainCategoryRequestDto mainCategoryRequestDto)
        {
            var mainCategory = new MainCategory
            {
                Name = mainCategoryRequestDto.Name,
                Slug = _slugUtil.GenerateSlug(mainCategoryRequestDto.Name)
            };

            await _mainCategoryRepository.AddAsync(mainCategory);
        }

        public async Task UpdateAsync(int id, MainCategoryRequestDto mainCategoryRequestDto)
        {
            var mainCategory = await _mainCategoryRepository.GetByIdAsync(id);
            if (mainCategory == null)
            {
                throw new KeyNotFoundException();
            }

            mainCategory.Name = mainCategoryRequestDto.Name;
            mainCategory.Slug = _slugUtil.GenerateSlug(mainCategoryRequestDto.Name);

            await _mainCategoryRepository.UpdateAsync(mainCategory);
        }

        public async Task DeleteAsync(int id)
        {
            var mainCategory = await _mainCategoryRepository.GetByIdAsync(id);
            if (mainCategory == null)
            {
                throw new KeyNotFoundException();
            }

            await _mainCategoryRepository.DeleteAsync(mainCategory);
        }

        public async Task<PagedDto<MainCategoryDto>> GetPagedAsync(MainCategoryFilterDto mainCategoryFilterDto)
        {
            var query = await _mainCategoryRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(mainCategoryFilterDto.Keyword))
            {
                query = query.Where(p => p.Name.Contains(mainCategoryFilterDto.Keyword));
            }

            if (!string.IsNullOrEmpty(mainCategoryFilterDto.OrderBy))
            {
                switch (mainCategoryFilterDto.OrderBy.ToLower())
                {
                    case "name":
                        query = mainCategoryFilterDto.IsDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                        break;
                    default:
                        break;
                }
            }

            var totalRecords = query.Count();
            var pagedItems = query.Skip(mainCategoryFilterDto.GetSkip())
                                    .Take(mainCategoryFilterDto.GetTake());

            IEnumerable<MainCategoryDto> mainCategoryDtos = pagedItems.Select(p => new MainCategoryDto
            {
                Id = p.Id,
                Name = p.Name,
                Slug = p.Slug
            }).ToList();

            return new PagedDto<MainCategoryDto>(totalRecords, mainCategoryDtos.ToList());
        }


        public async Task<bool> ExistsAsync(int id)
        {
            var mainCategory = await _mainCategoryRepository.GetByIdAsync(id);
            return mainCategory != null;
        }
    }
}
