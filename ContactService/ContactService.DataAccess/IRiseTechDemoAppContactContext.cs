using Microsoft.EntityFrameworkCore;
using RiseTechDemoApp.Domain.DBModels;

namespace ContactService.DataAccess;

public interface IRiseTechDemoAppContactContext
{
    DbSet<Person> People { get; set; }
    DbSet<ContactInfo> ContactInfo { get; set; }

    int SaveChanges();
}