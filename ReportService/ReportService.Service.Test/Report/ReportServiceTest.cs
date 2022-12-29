using Lamar;
using RiseTechDemoApp.Domain.DBModels;
using RiseTechDemoApp.Domain.DTO;
using ReportService.Service.Test.DI;

namespace ReportService.Service.Test
{
    public class ReportServiceTest : IClassFixture<LamarContainerFactory>
    {
        readonly IContainer _container;
        readonly IReportService _reportService;

        public ReportServiceTest(LamarContainerFactory factory)
        {
            _container = factory.Container;
            _reportService = _container.GetInstance<IReportService>();
        }

        [Fact]
        public void GetReports()
        {
            QueryParams<ReportData> queryParams = new() { Filter = new() };
            _reportService.GetReports(queryParams);
        }

        [Fact]
        public void GetReport()
        {
            _reportService.GetReport(Guid.NewGuid());
        }

        [Fact]
        public void Save()
        {
            _reportService.Save(new Report());
        }
    }
}