using MaleFashion.Server.Models.DTOs.OrderItem;
using MaleFashion.Server.Models.Entities;

namespace MaleFashion.Server.Models.DTOs.Order
{
    public class OrderRequestDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public string? Note { get; set; }
        public required string PaymentMethod { get; set; }
    }
}
