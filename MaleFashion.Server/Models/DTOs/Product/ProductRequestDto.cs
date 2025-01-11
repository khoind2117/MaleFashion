using System.ComponentModel.DataAnnotations.Schema;

namespace MaleFashion.Server.Models.DTOs.Product
{
    public class ProductRequestDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int? SubCategoryId { get; set; }
    }
}
