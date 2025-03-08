using MaleFashion.Server.Models.Entities;

namespace MaleFashion.Server.Models.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public string? Note { get; set; }
        public required string PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }

        public string? UserId { get; set; }

        public int? OrderStatusId { get; set; }
    }
}
