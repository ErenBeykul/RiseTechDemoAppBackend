namespace ReportService.Worker
{
    /// <summary>
    /// Rabbitmq ya Mesaj Gönderme ve Alma İşlemlerini İfade Eder
    /// </summary>
    public interface IQueueActions
    {
        /// <summary>
        /// Kuyrukta Bekleyen Rapor Talebi Varsa Rapor Hazırlar
        /// </summary>
        void GenerateReportIfAsked();
    }
}