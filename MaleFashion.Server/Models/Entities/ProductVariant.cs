using System.Drawing;

namespace MaleFashion.Server.Models.Entities
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public int Stock { get; set; }

        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public int ColorId { get; set; }
        public virtual Color? Color { get; set; }

        public int SizeId { get; set; }
        public virtual Size? Size { get; set; }

        public virtual ICollection<CartItem>? CartItems { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
        public virtual ICollection<Favorite>? Favorites { get; set; }
    }
}
