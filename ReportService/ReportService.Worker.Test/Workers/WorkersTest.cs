using Lamar;
using ReportService.Worker.Test.DI;

namespace ReportService.Worker.Test
{
    public class WorkersTest : IClassFixture<LamarContainerFactory>
    {
        readonly IContainer _container;
        readonly IWorkers _workers;

        public WorkersTest(LamarContainerFactory factory)
        {
            _container = factory.Container;
            _workers = _container.GetInstance<IWorkers>();
        }

        [Fact]
        public void GenerateReport()
        {
            _workers.GenerateReport(new Guid());
        }
    }
}