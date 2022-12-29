using ReportService.DataAccess;
using RiseTechDemoApp.Domain.DBModels;
using RiseTechDemoApp.Domain.Constants;
using RiseTechDemoApp.Domain.DTO;
using RiseTechDemoApp.Domain.Enums;
using RiseTechDemoApp.Domain.Extensions;

namespace ReportService.Service
{
    public class ReportService : IReportService
    {
        readonly IRiseTechDemoAppReportContext _context;

        public ReportService(IRiseTechDemoAppReportContext context)
        {
            _context = context;
        }

        public QueryData<ReportData> GetReports(QueryParams<ReportData> queryParams)
        {
            List<ReportData> reports = new();
            var dataReports = _context.Reports.AsQueryable();

            if (!string.IsNullOrEmpty(queryParams.Filter?.Date))
            {
                dataReports = dataReports.Where(x => x.CreateDate >= DateTime.Parse(queryParams.Filter.Date));
            }

            if (!string.IsNullOrEmpty(queryParams.Filter?.LastDate))
            {
                dataReports = dataReports.Where(x => x.CreateDate < DateTime.Parse(queryParams.Filter.LastDate).AddDays(1));
            }

            if (!string.IsNullOrEmpty(queryParams.SortField))
            {
                if (queryParams.SortOrder == "asc")
                {
                    if (queryParams.SortField == "date") dataReports = dataReports.OrderBy(x => x.CreateDate);
                }
                else if (queryParams.SortOrder == "desc")
                {
                    if (queryParams.SortField == "date") dataReports = dataReports.OrderByDescending(x => x.CreateDate);
                }
            }

            var pagedReports = dataReports.Skip((queryParams.PageNumber - 1) * queryParams.PageSize).Take(queryParams.PageSize);

            foreach (var item in pagedReports)
            {
                ReportData report = new()
                {
                    Id = item.Id,
                    Date = item.CreateDate.ToString("dd.MM.yyyy HH:mm"),
                    Status = ((int?)item.Status?.GetEnum<ReportStatus>()).ToString()
                };

                reports.Add(report);
            }

            QueryData<ReportData> queryData = new()
            {
                TotalCount = dataReports.Count(),
                Entities = reports
            };

            return queryData;
        }

        public Report GetReport(Guid id)
        {
            return _context.Reports.Find(id);
        }

        public Result Save(Report report)
        {
            Result result = new();

            if (report.Id == Guid.Empty)
            {
                #region Ekleme İşlemleri
                report.Id = Guid.NewGuid();
                report.CreateDate = DateTime.Now;
                report.Status = ReportStatus.Preparing.GetDisplayName();

                _context.Reports.Add(report);
                #endregion
            }
            else
            {
                #region Düzenleme İşlemleri
                Report dataReport = GetReport(report.Id);

                if (dataReport == null)
                {
                    result.Type = ResultName.Warning.ToLowerString();
                    result.Message = ResultMessages.NonExistingData;

                    return result;
                }

                dataReport.FilePath = report.FilePath;
                dataReport.CompleteDate = DateTime.Now;
                dataReport.Status = ReportStatus.Completed.GetDisplayName();

                _context.Reports.Update(dataReport);
                #endregion
            }

            if (_context.SaveChanges() > 0)
            {
                result.IsSuccess = true;
                result.Type = ResultName.Success.ToLowerString();
            }
            else
            {
                result.Type = ResultName.Error.ToLowerString();
                result.Message = ResultMessages.Error;
            }

            return result;
        }
    }
}