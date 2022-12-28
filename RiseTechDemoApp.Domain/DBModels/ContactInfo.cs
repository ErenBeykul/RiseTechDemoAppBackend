namespace RiseTechDemoApp.Domain.DBModels
{
    public class ContactInfo
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string Info { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Person Person { get; set; }
    }
}