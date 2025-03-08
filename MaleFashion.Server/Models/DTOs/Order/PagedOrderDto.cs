namespace MaleFashion.Server.Models.DTOs.Order
{
    public class PagedOrderDto
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public required string PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? OrderStatusId { get; set; }
    }
}
