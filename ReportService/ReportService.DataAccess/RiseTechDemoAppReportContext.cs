using ReportService.DataAccess.Mappings;
using RiseTechDemoApp.Domain.DBModels;
using Microsoft.EntityFrameworkCore;

namespace ReportService.DataAccess;

public class RiseTechDemoAppReportContext : DbContext, IRiseTechDemoAppReportContext
{
    public RiseTechDemoAppReportContext(DbContextOptions<RiseTechDemoAppReportContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ReportMap());

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}