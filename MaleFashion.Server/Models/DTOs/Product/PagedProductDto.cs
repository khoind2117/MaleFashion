using MaleFashion.Server.Models.DTOs.Color;
using MaleFashion.Server.Models.DTOs.ProductVariant;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaleFashion.Server.Models.DTOs.Product
{
    public class PagedProductDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public virtual ICollection<ColorDto>? Colors { get; set; }
    }
}
