namespace MaleFashion.Server.Models.Entities
{
    public class Color
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string ColorCode { get; set; }

        public virtual ICollection<ProductVariant>? ProductVariants { get; set; }
    }
}
