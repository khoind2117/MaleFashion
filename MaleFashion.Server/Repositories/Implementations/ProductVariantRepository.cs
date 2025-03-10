﻿using MaleFashion.Server.Data;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MaleFashion.Server.Repositories.Implementations
{
    public class ProductVariantRepository : GenericRepository<ApplicationDbContext, ProductVariant>, IProductVariantRepository
    {
        public ProductVariantRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProductVariant>> GetByProductIdAsync(int productId)
        {
            return await _dbSet.Where(pv => pv.ProductId == productId).ToListAsync();
        }

        public async Task<List<ProductVariant>> GetProductVariantsByIdsAsync(List<int> productVariantIds)
        {
            return await _dbSet.Where(pv => productVariantIds.Contains(pv.Id))
                                .Include(pv => pv.Product)
                                .Include(pv => pv.Color)
                                .Include(pv => pv.Size)
                                .AsSplitQuery()
                                .ToListAsync();
        }

    }
}
