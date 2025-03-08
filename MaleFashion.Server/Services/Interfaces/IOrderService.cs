using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Models.DTOs.Order;

namespace MaleFashion.Server.Services.Interfaces
{
    public interface IOrderService
    {
        Task<PagedDto<PagedOrderDto>> GetPagedAsync(OrderFilterDto orderFilterDto);
        Task<OrderDetailDto> GetByIdAsync(int id);
        Task<string> CheckOutAsync(OrderRequestDto orderRequestDto);
        Task CancelOrderAsync(int id);
    }
}
