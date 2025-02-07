using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Models.DTOs.Color;
using MaleFashion.Server.Models.DTOs.Product;
using MaleFashion.Server.Models.DTOs.ProductVariant;
using MaleFashion.Server.Models.DTOs.Size;
using MaleFashion.Server.Models.DTOs.SubCategory;
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

        public async Task<ProductDetailDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                throw new KeyNotFoundException();
            }

            var productDetailDto = new ProductDetailDto
            {
                Id = product.Id,
                Name = product.Name,
                Slug = product.Slug,
                Description = product.Description,
                Price = product.Price,
                SubCategoryDto = product.SubCategory != null ? new SubCategoryDto
                {
                    Id = product.SubCategory.Id,
                    Name = product.SubCategory.Name,
                    Slug = product.SubCategory.Slug,
                } : null,
                ProductVariantDtos = product.ProductVariants?.Select(pv => new ProductVariantDto
                {
                    Id = pv.Id,
                    Stock = pv.Stock,
                    ProductId = pv.ProductId,
                    ColorDto = pv.Color != null ? new ColorDto
                    {
                        Id = pv.Color.Id,
                        Name = pv.Color.Name,
                        ColorCode = pv.Color.ColorCode,
                    } : null,
                    SizeDto = pv.Size != null ? new SizeDto
                    {
                        Id = pv.Size.Id,
                        Name = pv.Size.Name,
                    } : null
                }).ToList()
            };

            return productDetailDto;
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

        public async Task<PagedDto<PagedProductDto>> GetPagedAsync(ProductFilterDto productFilterDto)
        {
            var pagedProducts = await _productRepository.GetPagedProductsAsync(productFilterDto);
            
            return pagedProducts;
        }


        public async Task<bool> ExistsAsync(int id)
        {
            return await _productRepository.ExistsAsync(id);
        }
    }
}
