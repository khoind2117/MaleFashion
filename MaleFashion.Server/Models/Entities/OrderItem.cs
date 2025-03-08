using System.ComponentModel.DataAnnotations.Schema;

namespace MaleFashion.Server.Models.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public int ProductVariantId { get; set; }
        public virtual ProductVariant? ProductVariant { get; set; }

        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }
    }
}
