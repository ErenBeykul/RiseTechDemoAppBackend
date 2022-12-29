using RiseTechDemoApp.Domain.DBModels;
using Microsoft.EntityFrameworkCore;

namespace ReportService.DataAccess;

public interface IRiseTechDemoAppReportContext
{
    DbSet<Report> Reports { get; set; }

    int SaveChanges();
}