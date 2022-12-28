using RiseTechDemoApp.Domain.DTO;
using RiseTechDemoApp.Domain.Extensions;

namespace RiseTechDemoApp.Domain.Helpers
{
    /// <summary>
    /// Enumlar İçin Yardımcı Metodları (Helpers) İfade Eder
    /// </summary>
    public static class EnumHelpers
    {
        /// <summary>
        /// Belli Bir Enumu SelectListItem Listesine Aktarır
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<SelectListItem> ToSelectListItems<T>() where T : Enum
        {
            List<SelectListItem> items = new();

            foreach (var value in Enum.GetValues(typeof(T)))
            {
                SelectListItem item = new()
                {
                    Value = ((int)value).ToString(),
                    Label = ((Enum)value).GetDisplayName()
                };

                items.Add(item);
            }

            return items;
        }
    }
}