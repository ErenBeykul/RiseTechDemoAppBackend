using System.ComponentModel.DataAnnotations;

namespace RiseTechDemoApp.Domain.Enums
{
    public enum ReportStatus
    {
        /// <summary>
        /// Hazırlanıyor
        /// </summary>
        [Display(Name = "Hazırlanıyor")]
        Preparing,

        /// <summary>
        /// Tamamlandı
        /// </summary>
        [Display(Name = "Tamamlandı")]
        Completed
    }
}