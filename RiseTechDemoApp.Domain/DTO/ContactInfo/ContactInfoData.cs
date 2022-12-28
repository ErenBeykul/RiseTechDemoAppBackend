namespace RiseTechDemoApp.Domain.DTO
{
    public class ContactInfoData
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string? PersonName { get; set; }
        public string? InfoType { get; set; }
        public string? Info { get; set; }
        public List<SelectListItem>? InfoTypes { get; set; }
    }
}