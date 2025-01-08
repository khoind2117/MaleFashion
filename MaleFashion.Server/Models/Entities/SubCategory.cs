namespace MaleFashion.Server.Models.Entities
{
    public class SubCategory
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }

        public int? MainCategoryId { get; set; }
        public virtual MainCategory? MainCategory { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
