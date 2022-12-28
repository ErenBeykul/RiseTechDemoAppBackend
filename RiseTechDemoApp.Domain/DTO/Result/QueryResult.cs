namespace RiseTechDemoApp.Domain.DTO
{
    public class QueryResult<T> : Result where T : class
    {
        public T? Entity { get; set; }
        public int TotalCount { get; set; }
        public List<T>? Entities { get; set; }
    }
}