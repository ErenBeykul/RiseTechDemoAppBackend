using Lamar;
using ContactService.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Service.Test.DI
{
    public class LamarMainRegistry : ServiceRegistry
    {
        public LamarMainRegistry(IConfiguration configuration)
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
                x.Assembly("ContactService.Service");
                x.Assembly("ContactService.Service.Test");
                x.Assembly("ContactService.DataAccess");
            });

            var connectionString = configuration.GetConnectionString("RiseTechDemoAppContactContext");
            var optionsBuilder = new DbContextOptionsBuilder<RiseTechDemoAppContactContext>();
            optionsBuilder.UseNpgsql(connectionString);

            For<IRiseTechDemoAppContactContext>().Use<RiseTechDemoAppContactContext>()
                  .Ctor<DbContextOptions<RiseTechDemoAppContactContext>>("options")
                              .Is(optionsBuilder.Options);

            For<IConfiguration>().Use(configuration);
        }
    }
}