namespace MaleFashion.Server.Models.Entities
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
