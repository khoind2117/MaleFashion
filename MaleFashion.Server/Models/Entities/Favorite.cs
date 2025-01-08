namespace MaleFashion.Server.Models.Entities
{
    public class Favorite
    {
        public int Id { get; set; }

        public string? UserId { get; set; }
        public virtual User? User { get; set; }

        public int ProductVariantId { get; set; }
        public virtual ProductVariant? ProductVariant { get; set; }
    }
}
