using RiseTechDemoApp.Domain.DBModels;
using Microsoft.EntityFrameworkCore;

namespace ContactService.DataAccess;

public interface IRiseTechDemoAppContactContext
{
    DbSet<Person> People { get; set; }
    DbSet<ContactInfo> ContactInfo { get; set; }

    int SaveChanges();
}