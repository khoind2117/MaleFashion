namespace MaleFashion.Server.Models.DTOs
{
    public class PagedDto<T>
    {
        public int TotalRecords { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public List<T>? Items { get; set; }
        public bool HasNextPage => PageNumber * PageSize < TotalRecords;
        public bool HasPreviousPage => PageNumber > 1;

        public PagedDto(int totalRecords, List<T> items)
        {
            TotalRecords = totalRecords;
            Items = items;
        }
    }

}
