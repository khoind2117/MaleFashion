namespace MaleFashion.Server.Models.Entities
{
    public class Size
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public virtual ICollection<ProductVariant>? ProductVariants { get; set; }
    }
}
