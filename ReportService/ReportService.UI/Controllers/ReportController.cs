using Microsoft.AspNetCore.Mvc;
using RiseTechDemoApp.Domain.Constants;
using RiseTechDemoApp.Domain.DBModels;
using RiseTechDemoApp.Domain.DTO;
using RiseTechDemoApp.Domain.Enums;
using RiseTechDemoApp.Domain.Extensions;
using ReportService.Service;
using ReportService.UI.Attributes;
using ReportService.Worker;

namespace ReportService.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(AuthAttribute))]
    public class ReportController : ControllerBase
    {
        readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Belli Bir Rapor Listesini Elde Eder
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost("GetReports")]
        public QueryResult<ReportData> GetReports(QueryParams<ReportData> queryParams)
        {
            QueryResult<ReportData> result = new();

            try
            {
                QueryData<ReportData> queryData = _reportService.GetReports(queryParams);
                result.Entities = queryData.Entities;
                result.TotalCount = queryData.TotalCount;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Type = ResultName.Error.ToLowerString();
                result.Message = ResultMessages.Error;
            }

            return result;
        }

        /// <summary>
        /// Belli Bir Raporu Elde Eder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public QueryResult<ReportData> GetReport(Guid id)
        {
            QueryResult<ReportData> result = new() { Entity = new ReportData() { ReportFile = Array.Empty<byte>() } };

            try
            {
                Report report = _reportService.GetReport(id);

                if (report != null)
                {
                    result.Entity.ReportFile = System.IO.File.ReadAllBytes(report.FilePath);
                    result.Entity.FileName = Path.GetFileName(report.FilePath);
                }                
            }
            catch (Exception ex)
            {
                result.Type = ResultName.Error.ToLowerString();
                result.Message = ResultMessages.Error;
            }

            return result;
        }

        /// <summary>
        /// Yeni Bir Rapor Oluþturur
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result Create()
        {
            Result result = new();

            try
            {
                // Yeni Bir Rapor Oluþturuluyor
                Report report = new();
                result = _reportService.Save(report);

                if (result.IsSuccess)
                {
                    // Oluþturulan Rapor Hazýrlanmak Üzere Kuyruða Gönderiliyor
                    QueueActions.Send(QueueName.Reports.ToString(), report.Id.ToString());
                    result.Message = ResultMessages.ReportPreparing;
                }
            }
            catch (Exception ex)
            {
                result.Type = ResultName.Error.ToLowerString();
                result.Message = ResultMessages.Error;
            }

            return result;
        }
    }
}