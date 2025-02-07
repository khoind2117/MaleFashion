using MaleFashion.Server.Models.DTOs.Color;
using MaleFashion.Server.Models.DTOs.Size;

namespace MaleFashion.Server.Models.DTOs.ProductVariant
{
    public class ProductVariantDto
    {
        public int Id { get; set; }
        public int Stock { get; set; }

        public int ProductId { get; set; }

        public ColorDto? ColorDto { get; set; }

        public SizeDto? SizeDto { get; set; }
    }
}
