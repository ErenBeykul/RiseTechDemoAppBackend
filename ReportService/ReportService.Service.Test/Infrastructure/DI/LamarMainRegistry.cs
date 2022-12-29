using Lamar;
using ReportService.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ReportService.Service.Test.DI
{
    public class LamarMainRegistry : ServiceRegistry
    {
        public LamarMainRegistry(IConfiguration configuration)
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
                x.Assembly("ReportService.Service");
                x.Assembly("ReportService.Service.Test");
                x.Assembly("ReportService.DataAccess");
            });

            var connectionString = configuration.GetConnectionString("RiseTechDemoAppReportContext");
            var optionsBuilder = new DbContextOptionsBuilder<RiseTechDemoAppReportContext>();
            optionsBuilder.UseNpgsql(connectionString);

            For<IRiseTechDemoAppReportContext>().Use<RiseTechDemoAppReportContext>()
                  .Ctor<DbContextOptions<RiseTechDemoAppReportContext>>("options")
                              .Is(optionsBuilder.Options);

            For<IConfiguration>().Use(configuration);
        }
    }
}