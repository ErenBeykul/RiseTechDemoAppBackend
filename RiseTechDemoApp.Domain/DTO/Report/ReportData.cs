namespace RiseTechDemoApp.Domain.DTO
{
    public class ReportData
    {
        public Guid Id { get; set; }
        public string? Date { get; set; }
        public string? Status { get; set; }
        public string? FileName { get; set; }
        public string? Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneNumberCount { get; set; }
        public string? LastDate { get; set; }
        public byte[]? ReportFile { get; set; }
    }
}