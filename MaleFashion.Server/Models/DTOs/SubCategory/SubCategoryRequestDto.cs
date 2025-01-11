namespace MaleFashion.Server.Models.DTOs.SubCategory
{
    public class SubCategoryRequestDto
    {
        public required string Name { get; set; }
        public int? MainCategoryId { get; set; }
    }
}
