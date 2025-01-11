using MaleFashion.Server.Models.Entities;

namespace MaleFashion.Server.Models.DTOs.Size
{
    public class SizeDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public virtual ICollection<Entities.ProductVariant>? ProductVariants { get; set; }
    }
}
