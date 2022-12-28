using ContactService.DataAccess.Mappings;
using Microsoft.EntityFrameworkCore;
using RiseTechDemoApp.Domain.DBModels;

namespace ContactService.DataAccess;

public class RiseTechDemoAppContactContext : DbContext, IRiseTechDemoAppContactContext
{
    public RiseTechDemoAppContactContext(DbContextOptions<RiseTechDemoAppContactContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Person> People { get; set; }
    public DbSet<ContactInfo> ContactInfo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersonMap());
        modelBuilder.ApplyConfiguration(new ContactInfoMap());

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}