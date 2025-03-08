using MaleFashion.Server.Models.DTOs.ProductVariant;

namespace MaleFashion.Server.Models.DTOs.CartItem
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductVariantId { get; set; }
        public ProductVariantDto? ProductVariantDto { get; set; }
        public int? CartId { get; set; }    
    }
}
