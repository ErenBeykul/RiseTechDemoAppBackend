using RiseTechDemoApp.Domain.DTO;

namespace ReportService.Worker
{
    public interface IWorkers
    {
        /// <summary>
        /// Rehber Raporu Üretir
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        Task GenerateReport(Guid reportId);



    }
}