using MaleFashion.Server.Models.DTOs.Order;
using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Models.Entities;

namespace MaleFashion.Server.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<PagedDto<PagedOrderDto>> GetPagedOrdersAsync(OrderFilterDto orderFilterDto, string userId);
        Task<OrderDetailDto?> GetOrderByIdAsync(int id, string userId);
    }
}
