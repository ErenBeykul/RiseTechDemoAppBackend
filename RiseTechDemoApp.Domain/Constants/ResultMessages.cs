namespace RiseTechDemoApp.Domain.Constants
{
    /// <summary>
    /// İşlem Sonucu Mesajlarını İfade Eder
    /// </summary>
    public static class ResultMessages
    {        
        /// <summary>
        /// İşlem Başarıyla Gerçekleştirilmiştir.
        /// </summary>
        public static readonly string Success = "İşlem Başarıyla Gerçekleştirilmiştir.";

        /// <summary>
        /// İşlem Sırasında Bir Hata Oluştu. Lütfen Sistem Yöneticiniz İle İrtibata Geçiniz!
        /// </summary>
        public static readonly string Error = "İşlem Sırasında Bir Hata Oluştu. Lütfen Sistem Yöneticiniz İle İrtibata Geçiniz!";

        /// <summary>
        /// Zorunlu Alanlar Boş Geçilemez. Lütfen Kontrol Ediniz!
        /// </summary>
        public static readonly string InputEmpty = "Zorunlu Alanlar Boş Geçilemez. Lütfen Kontrol Ediniz!";

        /// <summary>
        /// Sözkonusu Kayıt Bulunamadı. Lütfen Kontrol Ediniz!
        /// </summary>
        public static readonly string NonExistingData = "Sözkonusu Kayıt Bulunamadı. Lütfen Kontrol Ediniz!";

        /// <summary>
        /// Lütfen Kayıt Seçiniz!
        /// </summary>
        public static readonly string RecordSelection = "Lütfen Kayıt Seçiniz!";

        /// <summary>
        /// Rapor Hazırlanmaya Başlamıştır.
        /// </summary>
        public static readonly string ReportPreparing = "Rapor Hazırlanmaya Başlamıştır.";

        /// <summary>
        /// Rapor Servisine Bağlanırken Bir Hata Oluştu. Lütfen Sistem Yöneticiniz İle İrtibata Geçiniz!
        /// </summary>
        public static readonly string ReportServiceError = "Rapor Servisine Bağlanırken Bir Hata Oluştu. Lütfen Sistem Yöneticiniz İle İrtibata Geçiniz!";
    }
}