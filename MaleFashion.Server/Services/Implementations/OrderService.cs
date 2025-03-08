using MaleFashion.Server.Models.DTOs;
using MaleFashion.Server.Models.DTOs.Order;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Repositories.Interfaces;
using MaleFashion.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MaleFashion.Server.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IUnitOfWork unitOfWork,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetUserId()
        {
            return _userManager.GetUserId(_httpContextAccessor.HttpContext?.User)
                   ?? throw new UnauthorizedAccessException("User is not authenticated");
        }

        public async Task<PagedDto<PagedOrderDto>> GetPagedAsync(OrderFilterDto orderFilterDto)
        {
            var userId = GetUserId();
            return await _unitOfWork.OrderRepository.GetPagedOrdersAsync(orderFilterDto, userId);
        }

        public async Task<OrderDetailDto?> GetByIdAsync(int id)
        {
            var userId = GetUserId();
            return await _unitOfWork.OrderRepository.GetOrderByIdAsync(id, userId);
        }

        public async Task<string> CheckOutAsync(OrderRequestDto orderRequestDto)
        {
            var userId = GetUserId();
            var cart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);
            if (cart == null || cart.CartItems.Count == 0)
            {
                throw new Exception("Empty cart");
            }

            var pendingStatus = await _unitOfWork.OrderStatusRepository.GetOrderStatusByName("Pending");
            if (pendingStatus == null)
            {
                throw new Exception("Pending order status not found.");
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var order = new Order
                {
                    FirstName = orderRequestDto.FirstName,
                    LastName = orderRequestDto.LastName,
                    Address = orderRequestDto.Address,
                    PhoneNumber = orderRequestDto.PhoneNumber,
                    Email = orderRequestDto.Email,
                    Note = orderRequestDto.Note,
                    PaymentMethod = orderRequestDto.PaymentMethod,
                    CreatedAt = DateTime.Now,
                    UserId = userId,
                    OrderStatusId = pendingStatus.Id, // Pending
                    OrderItems = new List<OrderItem>()
                };

                await _unitOfWork.OrderRepository.AddAsync(order);
                await _unitOfWork.SaveChangesAsync();

                foreach (var item in cart.CartItems)
                {
                    var orderItem = new OrderItem
                    {
                        Quantity = item.Quantity,
                        UnitPrice = item.ProductVariant.Product.Price,
                        ProductVariantId = item.ProductVariantId,
                        OrderId = order.Id,
                    };
                    order.OrderItems.Add(orderItem);
                }

                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CartItemRepository.ClearCartItemsAsync(cart.Id);
                await _unitOfWork.SaveChangesAsync();

                string paymentUrl = string.Empty;

                switch (orderRequestDto.PaymentMethod)
                {
                    case "COD":
                        await _unitOfWork.CommitTransactionAsync();
                        return "Order placed successfully with COD";

                    //case "PayPal":
                    //    paymentUrl = await _payPalService.CreatePayment(order);
                    //    break;

                    //case "VNPay":
                    //    paymentUrl = await _vnPayService.CreatePayment(order);
                    //    break;

                    //case "MoMo":
                    //    paymentUrl = await _moMoService.CreatePayment(order);
                    //    break;

                    default:
                        throw new Exception("Invalid payment method.");
                }

                await _unitOfWork.CommitTransactionAsync();
                return paymentUrl;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task CancelOrderAsync(int id)
        {
            var userId = GetUserId();

            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null || order.UserId != userId)
            {
                throw new Exception("Order not found or access denied");
            }

            var pendingStatus = await _unitOfWork.OrderStatusRepository.GetOrderStatusByName("Pending");
            var cancelledStatus = await _unitOfWork.OrderStatusRepository.GetOrderStatusByName("Cancelled");

            if (pendingStatus == null || cancelledStatus == null)
            {
                throw new Exception("Order statuses not found");
            }

            if (order.OrderStatusId != pendingStatus.Id)
            {
                throw new Exception("Order cannot be cancelled because it is not in 'Pending' status");
            }

            order.OrderStatusId = cancelledStatus.Id;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
