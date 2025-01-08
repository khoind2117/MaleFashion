using System.ComponentModel.DataAnnotations.Schema;

namespace MaleFashion.Server.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public int? SubCategoryId { get; set; }
        public virtual SubCategory? SubCategory { get; set; }

        public virtual ICollection<ProductVariant>? ProductVariants { get; set; }
    }
}
