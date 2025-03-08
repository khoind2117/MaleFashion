using MaleFashion.Server.Models.Entities;

namespace MaleFashion.Server.Repositories.Interfaces
{
    public interface IOrderStatusRepository : IGenericRepository<OrderStatus>
    {
        public Task<OrderStatus?> GetOrderStatusByName(string orderStatusName);
    }
}
