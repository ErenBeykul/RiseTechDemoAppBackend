using Lamar;
using Microsoft.Extensions.Configuration;

namespace ReportService.Worker.Test.DI
{
    public class LamarContainerFactory : IDisposable
    {
        public IContainer Container { get; private set; }

        public LamarContainerFactory()
        {
            Container = new Container(_ => Initialize(_));
        }

        private static void Initialize(ServiceRegistry registry)
        {
            var config = new ConfigurationBuilder().Build();
            var lamar = new LamarMainRegistry(config);
            registry.AddRange(lamar);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}