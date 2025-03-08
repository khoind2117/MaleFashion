using MaleFashion.Server.Data;
using MaleFashion.Server.Models.DTOs.Order;
using MaleFashion.Server.Models.DTOs.OrderItem;
using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MaleFashion.Server.Repositories.Implementations
{
    public class OrderRepository : GenericRepository<ApplicationDbContext, Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<PagedDto<PagedOrderDto>> GetPagedOrdersAsync(OrderFilterDto orderFilterDto, string userId)
        {
            IQueryable<Order> query = _dbSet
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariant)
                        .ThenInclude(pv => pv.Product);

            //if (!string.IsNullOrEmpty(orderFilterDto.Status))
            //{
            //    query = query.Where(o => o.OrderStatusId == orderFilterDto.);
            //}

            var totalRecords = await query.CountAsync();
            var pagedOrders = await query.Skip(orderFilterDto.GetSkip())
                                        .Take(orderFilterDto.GetTake())
                                        .ToListAsync();

            var pagedOrderDtos = pagedOrders.Select(o => new PagedOrderDto
            {
                Id = o.Id,
                PaymentMethod = o.PaymentMethod,
                CreatedAt = o.CreatedAt,
                OrderStatusId = o.OrderStatusId,
            }).ToList();

            return new PagedDto<PagedOrderDto>(totalRecords, pagedOrderDtos);
        }

        public async Task<OrderDetailDto?> GetOrderByIdAsync(int id, string userId)
        {
            return await _context.Orders
                .Where(o => o.Id == id && o.UserId == userId)
                .Select(o => new OrderDetailDto
                {
                    Id = o.Id,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    Address = o.Address,
                    PhoneNumber = o.PhoneNumber,
                    Email = o.Email,
                    Note = o.Note,
                    PaymentMethod = o.PaymentMethod,
                    CreatedAt = o.CreatedAt,
                    OrderStatusId= o.OrderStatusId,
                    OrderStatus = o.OrderStatus,
                    OrderItemDtos = o.OrderItems.Select(oi => new OrderItemDto
                    {
                        ProductVariantId = oi.ProductVariantId,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }
    }
}
