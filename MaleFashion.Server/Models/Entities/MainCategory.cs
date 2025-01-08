namespace MaleFashion.Server.Models.Entities
{
    public class MainCategory
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }

        public virtual ICollection<SubCategory>? SubCategories { get; set; }
    }
}
