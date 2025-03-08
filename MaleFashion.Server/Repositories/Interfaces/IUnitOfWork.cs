namespace MaleFashion.Server.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }
        ICartRepository CartRepository { get; }
        ICartItemRepository CartItemRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderStatusRepository OrderStatusRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductVariantRepository ProductVariantRepository { get; }
        IMainCategoryRepository MainCategoryRepository { get; }
        ISubCategoryRepository SubCategoryRepository { get; }
        IColorRepository ColorRepository { get; }
        ISizeRepository SizeRepository { get; }

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<bool> SaveChangesAsync();
    }
}
