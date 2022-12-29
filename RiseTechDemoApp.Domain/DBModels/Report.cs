namespace RiseTechDemoApp.Domain.DBModels
{
    public class Report
    {
        public Guid Id { get; set; }
        public string? Status { get; set; }
        public string? FilePath { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? CompleteDate { get; set; }
    }
}