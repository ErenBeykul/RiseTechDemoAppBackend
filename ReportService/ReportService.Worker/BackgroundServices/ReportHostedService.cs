using Microsoft.Extensions.Hosting;

namespace ReportService.Worker
{
    /// <summary>
    /// Rapor Servisiyle Alakalı Arka Plan (Background) İşlemleri İfade Eder
    /// </summary>
    public class ReportHostedService : BackgroundService
    {
        readonly IQueueActions _actions;

        public ReportHostedService(IQueueActions actions)
        {
            _actions = actions;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);

                // Rapor Kuyruğu Dinlenerek Varsa Talep Rapor Üretiliyor
                _actions.GenerateReportIfAsked();
            }
        }
    }
}