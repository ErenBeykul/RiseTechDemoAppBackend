using ContactService.Service;
using Microsoft.AspNetCore.Mvc;
using RiseTechDemoApp.Domain.Constants;
using RiseTechDemoApp.Domain.DBModels;
using RiseTechDemoApp.Domain.DTO;
using RiseTechDemoApp.Domain.Enums;
using RiseTechDemoApp.Domain.Extensions;
using RiseTechDemoApp.Domain.Helpers;

namespace ContactService.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactInfoController : BaseController
    {
        readonly IPersonService _personService;
        readonly IContactInfoService _contactInfoService;

        public ContactInfoController(IPersonService personService, IContactInfoService contactInfoService)
        {
            _personService = personService;
            _contactInfoService = contactInfoService;
        }

        /// <summary>
        /// Rehberdeki Belli Bir Ýletiþim Bilgisi Listesini Elde Eder
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost("GetList")]
        public QueryResult<ContactInfoData> GetList(QueryParams<ContactInfoData> queryParams)
        {
            QueryResult<ContactInfoData> result = new() { Entity = new() };

            try
            {
                QueryData<ContactInfoData> queryData = _contactInfoService.GetList(queryParams);
                Person person = _personService.GetPerson(queryParams.Filter.PersonId);
                result.Entity.PersonName = person != null ? person.Name + " " + person.Surname : string.Empty;
                result.Entities = queryData.Entities;
                result.TotalCount = queryData.TotalCount;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Type = ResultName.Error.ToLowerString();
                result.Message = ResultMessages.Error;
            }

            return result;
        }

        /// <summary>
        /// Rehberdeki Bilgi Türü Listesini Elde Eder
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public QueryResult<ContactInfoData> GetInfoTypes()
        {
            QueryResult<ContactInfoData> result = new() { Entity = new() };

            try
            {
                result.Entity.InfoTypes = EnumHelpers.ToSelectListItems<InfoType>();
            }
            catch (Exception ex)
            {
                result.Type = ResultName.Error.ToLowerString();
                result.Message = ResultMessages.Error;
            }

            return result;
        }


        /// <summary>
        /// Yeni Bir Ýletiþim Bilgisi Elde Eder
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet("GetNewContactInfo/{personId}")]
        public QueryResult<ContactInfoData> GetNewContactInfo(Guid personId)
        {
            QueryResult<ContactInfoData> result = new();

            try
            {
                Person person = _personService.GetPerson(personId);
                string? personName = person != null ? person.Name + " " + person.Surname : string.Empty;

                result.Entity = new()
                {
                    PersonId = personId,
                    PersonName = personName,
                    Info = string.Empty,
                    InfoTypes = EnumHelpers.ToSelectListItems<InfoType>()
                };

                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Type = ResultName.Error.ToLowerString();
                result.Message = ResultMessages.Error;
            }

            return result;
        }

        /// <summary>
        /// Rehberdeki Belli Bir Ýletiþim Bilgisini Elde Eder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public QueryResult<ContactInfoData> GetContactInfo(Guid id)
        {
            QueryResult<ContactInfoData> result = new();

            try
            {
                ContactInfo info = _contactInfoService.GetContactInfoWithPerson(id);
                result.Entity = Mapper.Map<ContactInfoData>(info);
                result.Entity.PersonName = info.Person?.Name + " " + info.Person?.Surname;
                result.Entity.InfoType = ((int?)info.InfoType?.GetEnum<InfoType>()).ToString();
                result.Entity.InfoTypes = EnumHelpers.ToSelectListItems<InfoType>();
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Type = ResultName.Error.ToLowerString();
                result.Message = ResultMessages.Error;
            }

            return result;
        }

        /// <summary>
        /// Rehbere Belli Bir Ýletiþim Bilgisi Kaydeder
        /// </summary>
        /// <param name="infoData"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Save(ContactInfoData infoData)
        {
            Result result = new();

            // Zorunlu Alan Kontrolleri
            if (string.IsNullOrEmpty(infoData.InfoType) || string.IsNullOrEmpty(infoData.Info))
            {
                result.Type = ResultName.Warning.ToLowerString();
                result.Message = ResultMessages.InputEmpty;
                return result;
            }

            try
            {
                ContactInfo info = Mapper.Map<ContactInfo>(infoData);
                info.InfoType = infoData.InfoType.GetDisplayName<InfoType>();
                result = _contactInfoService.Save(info);
            }
            catch (Exception ex)
            {
                result.Type = ResultName.Error.ToLowerString();
                result.Message = ResultMessages.Error;
            }

            return result;
        }

        /// <summary>
        /// Rehberden Belli Bir Kiþi Listesini Siler
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("Delete")]
        public Result Delete(List<Guid> ids)
        {
            Result result = new();

            // Zorunlu Alan Kontrolleri
            if (ids.Count < 1)
            {
                result.Type = ResultName.Warning.ToLowerString();
                result.Message = ResultMessages.RecordSelection;
                return result;
            }

            try
            {
                result = _contactInfoService.Delete(ids);
            }
            catch (Exception ex)
            {
                result.Type = ResultName.Error.ToLowerString();
                result.Message = ResultMessages.Error;
            }

            return result;
        }
    }
}