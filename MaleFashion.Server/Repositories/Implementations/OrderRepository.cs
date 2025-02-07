using MaleFashion.Server.Data;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Repositories.Interfaces;

namespace MaleFashion.Server.Repositories.Implementations
{
    public class OrderRepository : Repository<ApplicationDbContext, Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
