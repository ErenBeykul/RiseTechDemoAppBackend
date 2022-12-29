using ContactService.Service;
using Microsoft.AspNetCore.Mvc;
using RiseTechDemoApp.Domain.Constants;
using RiseTechDemoApp.Domain.DTO;
using RiseTechDemoApp.Domain.Enums;
using RiseTechDemoApp.Domain.Extensions;
using System.Text.Json;

namespace ContactService.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : BaseController
    {
        readonly HttpClient _client;
        readonly IHttpClientFactory _factory;
        readonly IContactInfoService _contactInfoService;

        public ReportController(IHttpClientFactory factory, IContactInfoService contactInfoService)
        {
            _factory = factory;
            _client = _factory.CreateClient("ReportClient");
            _contactInfoService = contactInfoService;
        }

        /// <summary>
        /// Belli Bir Rapor Listesini Elde Eder
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost("GetReports")]
        public async Task<QueryResult<ReportData>> GetReports(QueryParams<ReportData> queryParams)
        {
            QueryResult<ReportData> result = new();

            try
            {
                HttpResponseMessage message = await _client.PostAsJsonAsync("/Report/GetReports", queryParams);

                if (message.IsSuccessStatusCode)
                {
                    string serializedResult = await message.Content.ReadAsStringAsync();
                    var nullableResult = JsonSerializer.Deserialize<QueryResult<ReportData>>(serializedResult, SerializerOptions);
                    result = nullableResult ?? new QueryResult<ReportData>();
                }
                else
                {
                    result.Type = ResultName.Error.ToLowerString();
                    result.Message = ResultMessages.ReportServiceError;
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
        /// Belli Bir Rehber Raporunun Verilerini Elde Eder
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public QueryResult<ReportData> GetReportData()
        {
            QueryResult<ReportData> result = new();

            try
            {
                result.Entities = _contactInfoService.GetReportData();
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
        public async Task<FileContentResult> GetReport(Guid id)
        {
            ReportData? report = new() { ReportFile = Array.Empty<byte>() };

            try
            {
                HttpResponseMessage message = await _client.GetAsync($"/Report/{id}");

                if (message.IsSuccessStatusCode)
                {
                    string serializedResult = await message.Content.ReadAsStringAsync();
                    var nullableResult = JsonSerializer.Deserialize<QueryResult<ReportData>>(serializedResult, SerializerOptions);
                    report = nullableResult != null ? nullableResult.Entity : report;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(report.ReportFile, contentType, report.FileName);
        }

        /// <summary>
        /// Yeni Bir Rapor Oluþturur
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> Create()
        {
            Result result = new();

            try
            {
                HttpResponseMessage message = await _client.PostAsync("/Report", null);

                if (message.IsSuccessStatusCode)
                {
                    string serializedResult = await message.Content.ReadAsStringAsync();
                    var nullableResult = JsonSerializer.Deserialize<Result>(serializedResult, SerializerOptions);
                    result = nullableResult ?? new Result();
                }
                else
                {
                    result.Type = ResultName.Error.ToLowerString();
                    result.Message = ResultMessages.ReportServiceError;
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