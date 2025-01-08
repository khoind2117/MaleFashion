using System.ComponentModel.DataAnnotations.Schema;

namespace MaleFashion.Server.Models.Entities
{
    public class Order
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
        public virtual User? User { get; set; }

        public int? OrderStatusId { get; set; }
        public virtual OrderStatus? OrderStatus { get; set; }

        public virtual ICollection<OrderItem>? OrderItems { get; set; }

        [NotMapped]
        public decimal TotalAmount => OrderItems.Sum(oi => oi.TotalPrice);
    }
}
