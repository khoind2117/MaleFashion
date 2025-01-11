namespace MaleFashion.Server.Models.DTOs.ProductVariant
{
    public class ProductVariantDto
    {
        public int Id { get; set; }
        public int Stock { get; set; }

        public int ProductId { get; set; }

        public int ColorId { get; set; }

        public int SizeId { get; set; }
    }
}
