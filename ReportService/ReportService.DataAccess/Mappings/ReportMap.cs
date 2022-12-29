using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiseTechDemoApp.Domain.DBModels;

namespace ReportService.DataAccess.Mappings
{
    public class ReportMap : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Status).HasColumnType("varchar(20)");
            builder.Property(t => t.FilePath).HasColumnType("varchar(150)");

            // Table & Field Mappings
            builder.ToTable("reports");
            builder.Property(t => t.Id).HasColumnName("id");
            builder.Property(t => t.Status).HasColumnName("status");
            builder.Property(t => t.FilePath).HasColumnName("file_path");
            builder.Property(t => t.CreateDate).HasColumnName("create_date");
            builder.Property(t => t.CompleteDate).HasColumnName("complete_date");
        }
    }
}