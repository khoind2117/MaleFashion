using MaleFashion.Server.Models.DTOs.CartItem;

namespace MaleFashion.Server.Models.DTOs.Cart
{
    public class CartDto
    {
        public string? UserId { get; set; }
        public Guid? BasketId { get; set; }
        public DateTime? LastUpdated { get; set; }
        public ICollection<CartItemDto>? CartItemDtos { get; set; }
    }
}
