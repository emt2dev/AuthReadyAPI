namespace AuthReadyAPI.DataLayer.DTOs.Pagination
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public int PageNumber { get;set;}
        public int RecordNumber { get;set;}
        public List<T> Records { get;set;}
    }
}
