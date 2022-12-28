namespace RiseTechDemoApp.Domain.DTO
{
    public class QueryData<T> where T : class
    {
        public int TotalCount { get; set; }
        public List<T> Entities { get; set; }
    }
}