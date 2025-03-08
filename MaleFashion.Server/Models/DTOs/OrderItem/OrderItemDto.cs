using System.ComponentModel.DataAnnotations.Schema;

namespace MaleFashion.Server.Models.DTOs.OrderItem
{
    public class OrderItemDto
    {
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public int ProductVariantId { get; set; }

        public int OrderId { get; set; }
    }
}
