using RiseTechDemoApp.Domain.DBModels;
using RiseTechDemoApp.Domain.DTO;

namespace ContactService.Service
{
    public interface IContactInfoService
    {
        /// <summary>
        /// Rehberdeki Belli Bir İletişim Bilgisi Listesini Elde Eder
        /// </summary>
        /// <returns></returns>
        QueryData<ContactInfoData> GetList(QueryParams<ContactInfoData> queryParams);

        /// <summary>
        /// Rehberdeki Belli Bir İletişim Bilgisini Elde Eder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ContactInfo GetContactInfo(Guid id);

        /// <summary>
        /// Rehberdeki Belli Bir Kişiyi ve İletişim Bilgisini Elde Eder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ContactInfo GetContactInfoWithPerson(Guid id);

        /// <summary>
        /// Belli Bir Rehber Raporunun Verilerini Elde Eder
        /// </summary>
        /// <returns></returns>
        List<ReportData> GetReportData();

        /// <summary>
        /// Rehbere Belli Bir İletişim Bilgisini Kaydeder
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Result Save(ContactInfo info);

        /// <summary>
        /// Rehberdeki Belli Bir İletişim Bilgisi Listesini Siler
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Result Delete(List<Guid> ids);
    }
}