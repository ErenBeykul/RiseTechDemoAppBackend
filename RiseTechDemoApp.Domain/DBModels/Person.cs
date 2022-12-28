namespace RiseTechDemoApp.Domain.DBModels
{
    public class Person
    {
        public Person()
        {
            ContactInfo = new List<ContactInfo>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Firm { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<ContactInfo> ContactInfo { get; set; }
    }
}