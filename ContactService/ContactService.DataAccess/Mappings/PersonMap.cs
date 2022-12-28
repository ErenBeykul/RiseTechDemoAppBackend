using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiseTechDemoApp.Domain.DBModels;

namespace ContactService.DataAccess.Mappings
{
    public class PersonMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Name).HasColumnType("varchar(20)");
            builder.Property(t => t.Surname).HasColumnType("varchar(20)");
            builder.Property(t => t.Firm).HasColumnType("varchar(50)");
            builder.Property(t => t.IsDeleted).HasDefaultValue(false);

            // Table & Field Mappings
            builder.ToTable("people");
            builder.Property(t => t.Id).HasColumnName("id");
            builder.Property(t => t.Name).HasColumnName("name");
            builder.Property(t => t.Surname).HasColumnName("surname");
            builder.Property(t => t.Firm).HasColumnName("firm");
            builder.Property(t => t.CreateDate).HasColumnName("create_date");
            builder.Property(t => t.UpdateDate).HasColumnName("update_date");
            builder.Property(t => t.IsDeleted).HasColumnName("is_deleted");

            // Relationships
            builder.HasMany(t => t.ContactInfo).WithOne(t => t.Person).HasForeignKey(d => d.PersonId);
        }
    }
}