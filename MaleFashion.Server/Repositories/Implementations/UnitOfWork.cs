using MaleFashion.Server.Data;
using MaleFashion.Server.Repositories.Interfaces;
using System;

namespace MaleFashion.Server.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IAccountRepository AccountRepository { get; }
        public ICartRepository CartRepository { get; }
        public ICartItemRepository CartItemRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IOrderStatusRepository OrderStatusRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IProductVariantRepository ProductVariantRepository { get; }
        public IMainCategoryRepository MainCategoryRepository { get; }
        public ISubCategoryRepository SubCategoryRepository { get; }
        public IColorRepository ColorRepository { get; }
        public ISizeRepository SizeRepository { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            IAccountRepository accountRepository,
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IOrderRepository orderRepository,
            IOrderStatusRepository orderStatusRepository,
            IProductRepository productRepository,
            IProductVariantRepository productVariantRepository,
            IMainCategoryRepository mainCategoryRepository,
            ISubCategoryRepository subCategoryRepository,
            IColorRepository colorRepository,
            ISizeRepository sizeRepository)
        {
            _context = context;
            AccountRepository = accountRepository;
            CartRepository = cartRepository;
            CartItemRepository = cartItemRepository;
            OrderRepository = orderRepository;
            OrderStatusRepository = orderStatusRepository;
            ProductRepository = productRepository;
            ProductVariantRepository = productVariantRepository;
            MainCategoryRepository = mainCategoryRepository;
            SubCategoryRepository = subCategoryRepository;
            ColorRepository = colorRepository;
            SizeRepository = sizeRepository;
        }

        public async Task BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();
        public async Task CommitTransactionAsync() => await _context.Database.CommitTransactionAsync();
        public async Task RollbackTransactionAsync() => await _context.Database.RollbackTransactionAsync();
        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}
