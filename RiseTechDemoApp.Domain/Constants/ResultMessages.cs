namespace RiseTechDemoApp.Domain.Constants
{
    /// <summary>
    /// İşlem Sonucu Mesajlarını İfade Eder
    /// </summary>
    public static class ResultMessages
    {        
        /// <summary>
        /// Evet
        /// </summary>
        public static readonly string Confirm = "Evet";

        /// <summary>
        /// Hayır
        /// </summary>
        public static readonly string Denial = "Hayır";

        /// <summary>
        /// İşlem Başarıyla Gerçekleştirilmiştir.
        /// </summary>
        public static readonly string Success = "İşlem Başarıyla Gerçekleştirilmiştir.";

        /// <summary>
        /// İşlem Sırasında Bir Hata Oluştu!
        /// </summary>
        public static readonly string Error = "İşlem Sırasında Bir Hata Oluştu. Lütfen Sistem Yöneticiniz İle İrtibata Geçiniz!";

        /// <summary>
        /// Kullanıcı Adı ve Şifre Alanları Boş Geçilemez. Lütfen Kontrol Ediniz!
        /// </summary>
        public static readonly string EmptyUsernameOrPassword = "Kullanıcı Adı ve Şifre Alanları Boş Geçilemez. Lütfen Kontrol Ediniz!";

        /// <summary>
        /// Kullanıcı Girişi Başarılıdır.
        /// </summary>
        public static readonly string LoginSuccess = "Kullanıcı Girişi Başarılıdır.";

        /// <summary>
        /// Kullanıcı Adı veya Şifreniz Hatalıdır. Lütfen Kontrol Ediniz!
        /// </summary>
        public static readonly string WrongUsernameOrPassword = "Kullanıcı Adı veya Şifreniz Hatalıdır. Lütfen Kontrol Ediniz!";

        /// <summary>
        /// Kullanıcı Hesabınız Pasif Durumdadır!
        /// </summary>
        public static readonly string PassiveUser = "Kullanıcı Hesabınız Pasif Durumdadır!";

        /// <summary>
        /// İzin Verilen IP Listesi Dışında Bir IP ile Giriş Yapmak İstemektesiniz. Lütfen Kontrol Ediniz!
        /// </summary>
        public static readonly string LoginWithUnAllowedIP = "İzin Verilen IP Listesi Dışında Bir IP ile Giriş Yapmak İstemektesiniz. Lütfen Kontrol Ediniz!";

        /// <summary>
        /// Bu İşlemi Yapmaya Yetkiniz Bulunmamaktadır!
        /// </summary>
        public static readonly string UnauthorizedAction = "Bu İşlemi Yapmaya Yetkiniz Bulunmamaktadır!";

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
        /// Silmek İstediğinize Emin misiniz?
        /// </summary>
        public static readonly string RecordDeletionConfirm = "Silmek İstediğinize Emin misiniz?";

        /// <summary>
        /// İlgili kayıt(lar) sistemden silinecektir.
        /// </summary>
        public static readonly string RecordDeletionConfirmDetail = "İlgili kayıt(lar) sistemden silinecektir.";

        /// <summary>
        /// İkon Alanı Boş Geçilemez. Lütfen Kontrol Ediniz!
        /// </summary>
        public static readonly string IconEmpty = "İkon Alanı Boş Geçilemez. Lütfen Kontrol Ediniz!";

        /// <summary>
        /// Link Alanı Boş Geçilemez. Lütfen Kontrol Ediniz!
        /// </summary>
        public static readonly string LinkEmpty = "Link Alanı Boş Geçilemez. Lütfen Kontrol Ediniz!";

        /// <summary>
        /// Şifre Alanı Boş Geçilemez. Lütfen Kontrol Ediniz!
        /// </summary>
        public static readonly string PasswordEmpty = "Şifre Alanı Boş Geçilemez. Lütfen Kontrol Ediniz!";

        /// <summary>
        /// Kullanıcı Grubu Alanı Boş Geçilemez. Lütfen Kontrol Ediniz!
        /// </summary>
        public static readonly string UserGroupEmpty = "Kullanıcı Grubu Alanı Boş Geçilemez. Lütfen Kontrol Ediniz!";

        /// <summary>
        /// Ders Kredisi 1 den Küçük Olamaz. Lütfen Kontrol Ediniz!
        /// </summary>
        public static readonly string NonPositiveCourseCredit = "Ders Kredisi 1 den Küçük Olamaz. Lütfen Kontrol Ediniz!";
    }
}