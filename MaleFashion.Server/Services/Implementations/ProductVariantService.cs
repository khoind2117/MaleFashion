using MaleFashion.Server.Models.DTOs.ProductVariant;
using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Services.Interfaces;
using MaleFashion.Server.Repositories.Interfaces;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Models.DTOs.Product;

namespace MaleFashion.Server.Services.Implementations
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IProductRepository _productRepository;

        public ProductVariantService(IProductVariantRepository productVariantRepository,
            IProductRepository productRepository)
        {
            _productVariantRepository = productVariantRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductVariantDto>> GetByProductIdAsync(int productId)
        {
            var productExists = await _productRepository.ExistsAsync(productId);
            if (!productExists)
            {
                throw new KeyNotFoundException();
            }

            var variants = await _productVariantRepository.GetByProductIdAsync(productId);

            return variants.Select(v => new ProductVariantDto
            {
                Id = v.Id,
                Stock = v.Stock,
                ProductId = v.ProductId,
                ColorId = v.ColorId,
                SizeId = v.SizeId,
            });
        }

        public async Task<ProductVariantDto?> GetByIdAsync(int variantId)
        {
            var variant = await _productVariantRepository.GetByIdAsync(variantId);

            if (variant == null)
            {
                return null;
            }

            return new ProductVariantDto
            {
                Id = variant.Id,
                Stock = variant.Stock,
                ProductId = variant.ProductId,
                ColorId = variant.ColorId,
                SizeId = variant.SizeId,
            };
        }

        public async Task AddAsync(ProductVariantRequestDto productVariantRequestDto)
        {
            var productExists = await _productRepository.ExistsAsync(productVariantRequestDto.ProductId);
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

            await _productVariantRepository.AddAsync(productVariant);
        }

        public async Task UpdateAsync(int variantId, ProductVariantRequestDto productVariantRequestDto)
        {
            var variant = await _productVariantRepository.GetByIdAsync(variantId);
            if (variant == null)
            {
                throw new KeyNotFoundException();
            }

            variant.Stock = productVariantRequestDto.Stock;
            variant.ColorId = productVariantRequestDto.ColorId;
            variant.SizeId = productVariantRequestDto.SizeId;

            await _productVariantRepository.UpdateAsync(variant);
        }

        public async Task DeleteAsync(int variantId)
        {
            var variant = await _productVariantRepository.GetByIdAsync(variantId);
            if (variant == null)
            {
                throw new KeyNotFoundException();
            }

            await _productVariantRepository.DeleteAsync(variant); 
        }
    }
}
