namespace MaleFashion.Server.Models.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        public string? UserId { get; set; }
        public virtual User? User { get; set; }

        public virtual ICollection<CartItem>? CartItems { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }
    }
}
