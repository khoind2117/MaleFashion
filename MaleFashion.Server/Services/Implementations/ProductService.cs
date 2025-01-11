using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Models.DTOs.Product;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Repositories.Implementations;
using MaleFashion.Server.Repositories.Interfaces;
using MaleFashion.Server.Services.Interfaces;
using MaleFashion.Server.Utilities;
using Microsoft.IdentityModel.Tokens;

namespace MaleFashion.Server.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly SlugUtil _slugUtil;

        public ProductService(IProductRepository productRepository,
            SlugUtil slugUtil)
        {
            _productRepository = productRepository;
            _slugUtil = slugUtil;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();

            var productDto = products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Slug = product.Slug,
                Description = product.Description,
                Price = product.Price,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                IsDeleted = product.IsDeleted
            });

            return productDto;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException();
            }

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Slug = product.Slug,
                Description = product.Description,
                Price = product.Price,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                IsDeleted = product.IsDeleted
            };

            return productDto;
        }

        public async Task AddAsync(ProductRequestDto productRequestDto)
        {
            var product = new Product
            {
                Name = productRequestDto.Name,
                Slug = _slugUtil.GenerateSlug(productRequestDto.Name),
                Description = productRequestDto.Description,
                Price = productRequestDto.Price,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false,
                SubCategoryId = productRequestDto.SubCategoryId,
            };

            await _productRepository.AddAsync(product);
        }

        public async Task UpdateAsync(int id, ProductRequestDto productRequestDto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException();
            }

            product.Name = productRequestDto.Name;
            product.Slug = _slugUtil.GenerateSlug(productRequestDto.Name);
            product.Description = productRequestDto.Description;
            product.Price = productRequestDto.Price;
            product.UpdatedAt = DateTime.Now;
            product.SubCategoryId = productRequestDto.SubCategoryId;

            await _productRepository.UpdateAsync(product);
        }

        public async Task SoftDeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException();
            }

            product.IsDeleted = true;

            await _productRepository.UpdateAsync(product);
        }

        public async Task HardDeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException();
            }

            await _productRepository.DeleteAsync(product);
        }

        public async Task<PagedDto<ProductDto>> GetPagedAsync(ProductFilterDto productFilterDto)
        {
            var query = await _productRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(productFilterDto.Keyword))
            {
                query = query.Where(p => p.Name.Contains(productFilterDto.Keyword));
            }

            if (!string.IsNullOrEmpty(productFilterDto.OrderBy))
            {
                switch (productFilterDto.OrderBy.ToLower())
                {
                    case "name":
                        query = productFilterDto.IsDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                        break;
                    default:
                        break;
                }
            }

            var totalRecords = query.Count();
            var pagedItems = query.Skip(productFilterDto.GetSkip())
                                    .Take(productFilterDto.GetTake());

            IEnumerable<ProductDto> productDtos = pagedItems.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Slug = p.Slug
            }).ToList();

            return new PagedDto<ProductDto>(totalRecords, productDtos.ToList());
        }


        public async Task<bool> ExistsAsync(int id)
        {
            return await _productRepository.ExistsAsync(id);
        }
    }
}
