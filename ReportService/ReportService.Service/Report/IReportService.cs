using RiseTechDemoApp.Domain.DBModels;
using RiseTechDemoApp.Domain.DTO;

namespace ReportService.Service
{
    public interface IReportService
    {
        /// <summary>
        /// Belli Bir Rapor Listesini Elde Eder
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        QueryData<ReportData> GetReports(QueryParams<ReportData> queryParams);

        /// <summary>
        /// Belli Bir Raporu Elde Eder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Report GetReport(Guid id);

        /// <summary>
        /// Belli Bir Raporu Kaydeder
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        Result Save(Report report);
    }
}