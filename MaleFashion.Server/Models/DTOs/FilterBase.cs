namespace MaleFashion.Server.Models.DTOs
{
    public class FilterBase
    {
        public string? Keyword { get; set; }
        public string? OrderBy { get; set; }
        public bool IsDescending { get; set; } = false;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string GetOrderDirection()
        {
            return IsDescending ? "DESC" : "ASC";
        }

        public int GetSkip()
        {
            return (PageIndex - 1) * PageSize;
        }

        public int GetTake()
        {
            return PageSize;
        }
    }

}
