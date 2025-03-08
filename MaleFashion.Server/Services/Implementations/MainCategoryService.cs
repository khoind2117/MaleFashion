using MaleFashion.Server.Models.DTOs.Color;
using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Repositories.Interfaces;
using MaleFashion.Server.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Models.DTOs.MainCategory;
using MaleFashion.Server.Utilities;
using MaleFashion.Server.Repositories.Implementations;

namespace MaleFashion.Server.Services.Implementations
{
    public class MainCategoryService : IMainCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SlugUtil _slugUtil;

        public MainCategoryService(IUnitOfWork unitOfWork,
            SlugUtil slugUtil)
        {
            _unitOfWork = unitOfWork;
            _slugUtil = slugUtil;
        }

        public async Task<IEnumerable<MainCategoryDto>> GetAllAsync()
        {
            var mainCategories = await _unitOfWork.MainCategoryRepository.GetAllAsync();

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
            var mainCategory = await _unitOfWork.MainCategoryRepository.GetByIdAsync(id);
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

            await _unitOfWork.MainCategoryRepository.AddAsync(mainCategory);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, MainCategoryRequestDto mainCategoryRequestDto)
        {
            var mainCategory = await _unitOfWork.MainCategoryRepository.GetByIdAsync(id);
            if (mainCategory == null)
            {
                throw new KeyNotFoundException();
            }

            mainCategory.Name = mainCategoryRequestDto.Name;
            mainCategory.Slug = _slugUtil.GenerateSlug(mainCategoryRequestDto.Name);

            await _unitOfWork.MainCategoryRepository.UpdateAsync(mainCategory);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var mainCategory = await _unitOfWork.MainCategoryRepository.GetByIdAsync(id);
            if (mainCategory == null)
            {
                throw new KeyNotFoundException();
            }

            await _unitOfWork.MainCategoryRepository.DeleteAsync(mainCategory);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedDto<MainCategoryDto>> GetPagedAsync(MainCategoryFilterDto mainCategoryFilterDto)
        {
            var query = await _unitOfWork.MainCategoryRepository.GetAllAsync();

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
            var mainCategory = await _unitOfWork.MainCategoryRepository.GetByIdAsync(id);
            return mainCategory != null;
        }
    }
}
