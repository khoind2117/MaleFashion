using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MaleFashion.Server.Repositories.Implementations
{
    public class GenericRepository<TDbContext, TEntity> : IGenericRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : class
    {
        protected readonly TDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(TDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public async Task<TEntity?> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return true;
            }
            return false;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return true; 
        }

        public async Task<bool> ExistsAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity != null;
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity?>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
