namespace RiseTechDemoApp.Domain.DTO
{
    public class QueryParams<T> where T : class
    {
        public T Filter { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortField { get; set; }
        public string SortOrder { get; set; }
    }
}