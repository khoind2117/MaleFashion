using MaleFashion.Server.Models.DTOs.ProductVariant;
using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Services.Interfaces;
using MaleFashion.Server.Repositories.Interfaces;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Models.DTOs.Product;
using MaleFashion.Server.Models.DTOs.Color;
using MaleFashion.Server.Models.DTOs.Size;

namespace MaleFashion.Server.Services.Implementations
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductVariantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProductVariantDto>> GetProductVariantsByIdsAsync(List<int> productVariantIds)
        {
            var productVariants = await _unitOfWork.ProductVariantRepository.GetProductVariantsByIdsAsync(productVariantIds);
            if (productVariants == null || !productVariants.Any())
            {
                return new List<ProductVariantDto>();
            }

            var productVariantDtos = productVariants.Select(pv => new ProductVariantDto
            {
                Id = pv.Id,
                Stock = pv.Stock,
                ProductDto = new ProductDto
                {
                    Id = pv.Product.Id,
                    Name = pv.Product.Name,
                    Price = pv.Product.Price,
                    IsActive = pv.Product.IsActive,
                },
                ColorDto = new ColorDto
                {
                    Id = pv.Color.Id,
                    Name = pv.Color.Name,
                    ColorCode = pv.Color.ColorCode,
                },
                SizeDto = new SizeDto
                {
                    Id = pv.Size.Id,
                    Name = pv.Size.Name,
                }
            }).ToList();

            return productVariantDtos;
        }

        public async Task<IEnumerable<ProductVariantDto>> GetByProductIdAsync(int productId)
        {
            var productExists = await _unitOfWork.ProductRepository.ExistsAsync(productId);
            if (!productExists)
            {
                throw new KeyNotFoundException();
            }

            var variants = await _unitOfWork.ProductVariantRepository.GetByProductIdAsync(productId);

            return variants.Select(pv => new ProductVariantDto
            {
                Id = pv.Id,
                Stock = pv.Stock,
                ProductDto = new ProductDto
                {
                    Id = pv.Product.Id,
                    Name = pv.Product.Name,
                    Price = pv.Product.Price,
                    IsActive = pv.Product.IsActive,
                },
            });
        }

        public async Task<ProductVariantDto?> GetByIdAsync(int variantId)
        {
            var variant = await _unitOfWork.ProductVariantRepository.GetByIdAsync(variantId);

            if (variant == null)
            {
                return null;
            }

            return new ProductVariantDto
            {
                Id = variant.Id,
                Stock = variant.Stock,
                ProductDto = new ProductDto
                {
                    Id = variant.Product.Id,
                    Name = variant.Product.Name,
                    Price = variant.Product.Price,
                    IsActive = variant.Product.IsActive,
                },
            };
        }

        public async Task AddAsync(ProductVariantRequestDto productVariantRequestDto)
        {
            var productExists = await _unitOfWork.ProductRepository.ExistsAsync(productVariantRequestDto.ProductId);
            if (!productExists)
            {
                throw new KeyNotFoundException();
            }

            var productVariant = new ProductVariant
            {
                Stock = productVariantRequestDto.Stock,
                ProductId = productVariantRequestDto.ProductId,
                ColorId = productVariantRequestDto.ColorId,
                SizeId = productVariantRequestDto.SizeId,
            };

            await _unitOfWork.ProductVariantRepository.AddAsync(productVariant);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(int variantId, ProductVariantRequestDto productVariantRequestDto)
        {
            var variant = await _unitOfWork.ProductVariantRepository.GetByIdAsync(variantId);
            if (variant == null)
            {
                throw new KeyNotFoundException();
            }

            variant.Stock = productVariantRequestDto.Stock;
            variant.ColorId = productVariantRequestDto.ColorId;
            variant.SizeId = productVariantRequestDto.SizeId;

            await _unitOfWork.ProductVariantRepository.UpdateAsync(variant);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int variantId)
        {
            var variant = await _unitOfWork.ProductVariantRepository.GetByIdAsync(variantId);
            if (variant == null)
            {
                throw new KeyNotFoundException();
            }

            await _unitOfWork.ProductVariantRepository.DeleteAsync(variant);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
