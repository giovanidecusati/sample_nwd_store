namespace BuildingBlock.Core.Paging
{
    public class PagedRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public bool Ascending { get; set; }
    }
}
