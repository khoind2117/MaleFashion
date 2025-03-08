using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Models.DTOs.SubCategory;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Repositories.Implementations;
using MaleFashion.Server.Repositories.Interfaces;
using MaleFashion.Server.Services.Interfaces;
using MaleFashion.Server.Utilities;
using Microsoft.IdentityModel.Tokens;

namespace MaleFashion.Server.Services.Implementations
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SlugUtil _slugUtil;

        public SubCategoryService(IUnitOfWork unitOfWork,
            SlugUtil slugUtil)
        {
            _unitOfWork = unitOfWork;
            _slugUtil = slugUtil;
        }

        public async Task<IEnumerable<SubCategoryDto>> GetAllAsync()
        {
            var SubCategories = await _unitOfWork.SubCategoryRepository.GetAllAsync();

            var subCategoryDtos = SubCategories.Select(subCategory => new SubCategoryDto
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                Slug = subCategory.Slug
            });

            return subCategoryDtos;
        }

        public async Task<SubCategoryDto> GetByIdAsync(int id)
        {
            var subCategory = await _unitOfWork.SubCategoryRepository.GetByIdAsync(id);
            if (subCategory == null)
            {
                throw new KeyNotFoundException();
            }

            var subCategoryDto = new SubCategoryDto
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                Slug = subCategory.Slug
            };

            return subCategoryDto;
        }

        public async Task AddAsync(SubCategoryRequestDto subCategoryRequestDto)
        {
            if (!subCategoryRequestDto.MainCategoryId.HasValue)
            {
                throw new ArgumentNullException();
            }

            var mainCategory = await _unitOfWork.MainCategoryRepository.GetByIdAsync(subCategoryRequestDto.MainCategoryId.Value);
            if (mainCategory == null)
            {
                throw new KeyNotFoundException();
            }

            var subCategory = new SubCategory
            {
                Name = subCategoryRequestDto.Name,
                Slug = _slugUtil.GenerateSlug(subCategoryRequestDto.Name),
                MainCategoryId = subCategoryRequestDto.MainCategoryId.Value
            };

            await _unitOfWork.SubCategoryRepository.AddAsync(subCategory);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, SubCategoryRequestDto subCategoryRequestDto)
        {
            var subCategory = await _unitOfWork.SubCategoryRepository.GetByIdAsync(id);
            if (subCategory == null)
            {
                throw new KeyNotFoundException();
            }

            if (subCategoryRequestDto.MainCategoryId.HasValue)
            {
                var mainCategory = await _unitOfWork.MainCategoryRepository.GetByIdAsync(subCategoryRequestDto.MainCategoryId.Value);
                if (mainCategory == null)
                {
                    throw new KeyNotFoundException();
                }
                subCategory.MainCategoryId = subCategoryRequestDto.MainCategoryId.Value;
            }

            subCategory.Name = subCategoryRequestDto.Name;
            subCategory.Slug = _slugUtil.GenerateSlug(subCategoryRequestDto.Name);

            await _unitOfWork.SubCategoryRepository.UpdateAsync(subCategory);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var subCategory = await _unitOfWork.SubCategoryRepository.GetByIdAsync(id);
            if (subCategory == null)
            {
                throw new KeyNotFoundException();
            }

            await _unitOfWork.SubCategoryRepository.DeleteAsync(subCategory);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedDto<SubCategoryDto>> GetPagedAsync(SubCategoryFilterDto subCategoryFilterDto)
        {
            var query = await _unitOfWork.SubCategoryRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(subCategoryFilterDto.Keyword))
            {
                query = query.Where(p => p.Name.Contains(subCategoryFilterDto.Keyword));
            }

            if (!string.IsNullOrEmpty(subCategoryFilterDto.OrderBy))
            {
                switch (subCategoryFilterDto.OrderBy.ToLower())
                {
                    case "name":
                        query = subCategoryFilterDto.IsDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                        break;
                    default:
                        break;
                }
            }

            var totalRecords = query.Count();
            var pagedItems = query.Skip(subCategoryFilterDto.GetSkip())
                                    .Take(subCategoryFilterDto.GetTake());

            IEnumerable<SubCategoryDto> subCategoryDtos = pagedItems.Select(p => new SubCategoryDto
            {
                Id = p.Id,
                Name = p.Name,
                Slug = p.Slug
            }).ToList();

            return new PagedDto<SubCategoryDto>(totalRecords, subCategoryDtos.ToList());
        }


        public async Task<bool> ExistsAsync(int id)
        {
            var subCategory = await _unitOfWork.SubCategoryRepository.GetByIdAsync(id);
            return subCategory != null;
        }
    }
}
