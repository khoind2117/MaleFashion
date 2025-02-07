using MaleFashion.Server.Models.DTOs.Product;
using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Models.DTOs.Order;

namespace MaleFashion.Server.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDetailDto> GetByIdAsync(int id);
        Task AddAsync(OrderRequestDto orderRequestDto);
        Task UpdateAsync(int id, OrderRequestDto orderRequestDto);

        Task<PagedDto<PagedOrderDto>> GetPagedAsync(OrderFilterDto orderFilterDto);
        Task<bool> ExistsAsync(int id);
    }
}
