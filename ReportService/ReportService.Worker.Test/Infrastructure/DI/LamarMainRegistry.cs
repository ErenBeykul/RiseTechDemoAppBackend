using Lamar;
using ReportService.DataAccess;
using Microsoft.Extensions.Configuration;

namespace ReportService.Worker.Test.DI
{
    public class LamarMainRegistry : ServiceRegistry
    {
        public LamarMainRegistry(IConfiguration configuration)
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
                x.Assembly("ReportService.Worker");
                x.Assembly("ReportService.Service");
            });
            
            For<IConfiguration>().Use(configuration);
            For<IHttpClientFactory>().Use(service => service.GetInstance<IHttpClientFactory>());
            For<IRiseTechDemoAppReportContext>().Use<RiseTechDemoAppReportContext>();
        }
    }
}