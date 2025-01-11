namespace MaleFashion.Server.Models.DTOs.ProductVariant
{
    public class ProductVariantRequestDto
    {
        public int Stock { get; set; }
        public int ProductId { get; set; }

        public int ColorId { get; set; }

        public int SizeId { get; set; }
    }
}
