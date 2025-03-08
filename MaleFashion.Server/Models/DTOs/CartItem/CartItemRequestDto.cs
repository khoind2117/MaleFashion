using System.ComponentModel.DataAnnotations.Schema;

namespace MaleFashion.Server.Models.DTOs.CartItem
{
    public class CartItemRequestDto
    {
        public int Quantity { get; set; }
        public int ProductVariantId { get; set; }
    }
}
