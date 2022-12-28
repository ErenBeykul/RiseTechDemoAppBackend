using RiseTechDemoApp.Domain.DBModels;
using RiseTechDemoApp.Domain.DTO;

namespace ContactService.Service
{
    public interface IPersonService
    {
        /// <summary>
        /// Rehberdeki Belli Bir Kişi Listesini Elde Eder
        /// </summary>
        /// <returns></returns>
        QueryData<PersonData> GetPeople(QueryParams<PersonData> queryParams);

        /// <summary>
        /// Rehberdeki Belli Bir Kişiyi Elde Eder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Person GetPerson(Guid id);

        /// <summary>
        /// Rehbere Belli Bir Kişiyi Kaydeder
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        Result Save(Person person);

        /// <summary>
        /// Rehberdeki Belli Bir Kişi Listesini Siler
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Result Delete(List<Guid> ids);
    }
}