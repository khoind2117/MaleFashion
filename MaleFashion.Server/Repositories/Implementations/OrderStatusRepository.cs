using MaleFashion.Server.Data;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MaleFashion.Server.Repositories.Implementations
{
    public class OrderStatusRepository : GenericRepository<ApplicationDbContext, OrderStatus>, IOrderStatusRepository
    {
        public OrderStatusRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<OrderStatus?> GetOrderStatusByName(string orderStatusName)
        {
            return await _context.OrderStatuses
                                    .FirstOrDefaultAsync(os => os.Name == orderStatusName);
        }
    }
}
