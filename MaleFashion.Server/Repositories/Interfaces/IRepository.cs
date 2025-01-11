namespace MaleFashion.Server.Repositories.Interfaces
{
    public interface IRepository<TEntity> 
        where TEntity : class
    {
        Task<TEntity?> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(object id);
        Task<bool> ExistsAsync(object id);
        Task<TEntity?> GetByIdAsync(object id);
        Task<IEnumerable<TEntity?>> GetAllAsync();

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
