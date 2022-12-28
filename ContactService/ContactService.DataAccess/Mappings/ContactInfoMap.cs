using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiseTechDemoApp.Domain.DBModels;

namespace ContactService.DataAccess.Mappings
{
    public class ContactInfoMap : IEntityTypeConfiguration<ContactInfo>
    {
        public void Configure(EntityTypeBuilder<ContactInfo> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Phone).HasColumnType("varchar(20)");
            builder.Property(t => t.Email).HasColumnType("varchar(50)");
            builder.Property(t => t.Location).HasColumnType("varchar(20)");
            builder.Property(t => t.Info).HasColumnType("varchar(1000)");
            builder.Property(t => t.IsDeleted).HasDefaultValue(false);

            // Table & Field Mappings
            builder.ToTable("contact_info");
            builder.Property(t => t.Id).HasColumnName("id");
            builder.Property(t => t.PersonId).HasColumnName("person_id");
            builder.Property(t => t.Phone).HasColumnName("phone");
            builder.Property(t => t.Email).HasColumnName("email");
            builder.Property(t => t.Location).HasColumnName("location");
            builder.Property(t => t.Info).HasColumnName("info");
            builder.Property(t => t.CreateDate).HasColumnName("create_date");
            builder.Property(t => t.UpdateDate).HasColumnName("Update_date");
            builder.Property(t => t.IsDeleted).HasColumnName("is_deleted");

            // Relationships
            builder.HasOne(t => t.Person).WithMany(t => t.ContactInfo).HasForeignKey(d => d.PersonId);
        }
    }
}