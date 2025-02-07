using MaleFashion.Server.Data;
using MaleFashion.Server.Models.DTOs.Product;
using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MaleFashion.Server.Models.DTOs.ProductVariant;
using MaleFashion.Server.Models.DTOs.Color;

namespace MaleFashion.Server.Repositories.Implementations
{
    public class ProductRepository : Repository<ApplicationDbContext, Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Product?> GetProductById(int id)
        {
            return await _dbSet
                .Include(p => p.SubCategory)
                .Include(p => p.ProductVariants!)
                    .ThenInclude(pv => pv.Color)
                .Include(p => p.ProductVariants!)
                    .ThenInclude(pv => pv.Size)
                .AsSplitQuery()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PagedDto<PagedProductDto>> GetPagedProductsAsync(ProductFilterDto productFilterDto)
        {
            IQueryable<Product> query = _dbSet;

            query = query.Include(p => p.ProductVariants)
                            .ThenInclude(pv => pv.Color);

            if (!string.IsNullOrEmpty(productFilterDto.Keyword))
            {
                query = query.Where(p => p.Name.Contains(productFilterDto.Keyword));
            }   

            if (!string.IsNullOrEmpty(productFilterDto.OrderBy))
            {
                switch (productFilterDto.OrderBy.ToLower()) 
                {
                    case "new-arrivals":
                        query = query.OrderByDescending(p => p.CreatedAt);
                        break;
                    case "low-to-high":
                        query = query.OrderBy(p => p.Price);
                        break;
                    case "high-to-low":
                        query = query.OrderByDescending(p => p.Price);
                        break;

                    default:
                        break;
                }
            }

            var totalRecords = await query.CountAsync();

            var pagedItems = await query.Skip(productFilterDto.GetSkip())
                                        .Take(productFilterDto.GetTake())
                                        .ToListAsync();

            var pagedProductDtos = pagedItems.Select(p => new PagedProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Slug = p.Slug,
                Price = p.Price,
                Colors = p.ProductVariants?
                    .GroupBy(pv => pv.Color)
                    .Select(group => new ColorDto
                    {
                        Id = group.Key.Id,
                        Name = group.Key.Name,
                        ColorCode = group.Key.ColorCode,
                    })
                    .ToList()
            }).ToList();

            return new PagedDto<PagedProductDto>(totalRecords, pagedProductDtos);
        }
    }
}
