using System.ComponentModel.DataAnnotations;

namespace RiseTechDemoApp.Domain.Enums
{
    public enum InfoType
    {
        /// <summary>
        /// Telefon
        /// </summary>
        [Display(Name = "Telefon")]
        Phone,

        /// <summary>
        /// Email
        /// </summary>
        [Display(Name = "Email")]
        Email,

        /// <summary>
        /// Konum
        /// </summary>
        [Display(Name = "Konum")]
        Location
    }
}