using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RiseTechDemoApp.Domain.Extensions
{
    /// <summary>
    /// Enum Uzantılarını (Extension) İfade Eder
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Belli Bir Enum Değerinin ... Elde Eder
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private static TAttribute? GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            return value.GetType().GetMember(value.ToString()).First().GetCustomAttribute<TAttribute>();
        }

        /// <summary>
        /// Belli Bir Enum Değerinin Gösterim Adını Elde Eder
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum value)
        {
            return value.GetAttribute<DisplayAttribute>().Name;
        }

        /// <summary>
        /// Belli Bir Enum Değerini Küçük Harfli Dizgi (String) Eşdeğerine Çevirir
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToLowerString(this Enum value)
        {
            return value.ToString().ToLower();
        }
    }
}