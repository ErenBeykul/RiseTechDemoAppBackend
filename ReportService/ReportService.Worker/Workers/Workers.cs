using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RiseTechDemoApp.Domain.DBModels;
using RiseTechDemoApp.Domain.DTO;
using ReportService.Service;
using System.Text.Json;

namespace ReportService.Worker
{
    public class Workers : IWorkers
    {
        readonly HttpClient _client;
        readonly IHttpClientFactory _factory;
        readonly IConfiguration _configuration;
        readonly IReportService _reportService;

        public Workers(IHttpClientFactory factory, IConfiguration configuration, IReportService reportService)
        {
            _factory = factory;
            _configuration = configuration;
            _reportService = reportService;
            _client = _factory.CreateClient("ContactClient");
        }

        public async Task GenerateReport(Guid reportId)
        {
            try
            {
                HttpResponseMessage message = await _client.GetAsync("/Report");
                string serializedData = await message.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
                var nullableData = JsonSerializer.Deserialize<QueryData<ReportData>>(serializedData, options);
                List<ReportData>? reportData = nullableData != null ? nullableData.Entities : new List<ReportData>();

                string? fileFolder = _configuration.GetSection("ReportsFolder").Value?.ToString();
                string fileName = "Rehber Raporu " + DateTime.Now.ToString("yyyyMMddHHmmss");
                using ExcelPackage package = new(fileFolder + fileName + ".xlsx");
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sayfa 1");

                // Başlıklar Ekleniyor
                worksheet.Column(1).Width = 25;
                worksheet.Column(2).Width = 15;
                worksheet.Column(3).Width = 22;
                worksheet.Cells[1, 1, 1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 1, 1, 3].Style.Font.Bold = true;
                worksheet.Cells[1, 1].Value = "Konum";
                worksheet.Cells[1, 2].Value = "Kişi Sayısı";
                worksheet.Cells[1, 3].Value = "Telefon Numarası Sayısı";

                // Satırlar Ekleniyor
                for (int i = 0; i < reportData?.Count; i++)
                {
                    worksheet.Cells[i + 2, 1, i + 2, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, 1].Value = reportData?[i].Location;
                    worksheet.Cells[i + 2, 2].Value = reportData?[i].PersonCount;
                    worksheet.Cells[i + 2, 3].Value = reportData?[i].PhoneNumberCount;
                }

                // Dosya Kaydediliyor
                package.Save();

                // Raporun Bilgileri Rapor Veritabanında Tamamlandı Olarak Güncelleniyor
                Report report = _reportService.GetReport(reportId);
                report.FilePath = fileFolder + fileName + ".xlsx";
                _reportService.Save(report);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}