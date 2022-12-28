using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RiseTechDemoApp.Domain.Constants;
using RiseTechDemoApp.Domain.DBModels;
using RiseTechDemoApp.Domain.DTO;
using RiseTechDemoApp.Domain.Enums;
using RiseTechDemoApp.Domain.Extensions;
using ContactService.Service;

namespace ContactService.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        readonly IMapper _mapper;
        readonly IPersonService _personService;

        public PersonController(IMapper mapper, IPersonService personService)
        {
            _mapper = mapper;
            _personService = personService;
        }

        /// <summary>
        /// Rehberdeki Belli Bir Kiþi Listesini Elde Eder
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost("GetPeople")]
        public QueryResult<PersonData> GetPeople(QueryParams<PersonData> queryParams)
        {
            QueryResult<PersonData> result = new();

            try
            {
                QueryData<PersonData> queryData = _personService.GetPeople(queryParams);
                result.Entities = queryData.Entities;
                result.TotalCount = queryData.TotalCount;
            }
            catch (Exception ex)
            {
                result.Type = ResultName.Error.ToLowerString();
                result.Message = ResultMessages.Error;
            }

            return result;
        }

        /// <summary>
        /// Yeni Bir Kiþi Elde Eder
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public QueryResult<PersonData> GetNewPerson()
        {
            QueryResult<PersonData> result = new();

            try
            {
                result.Entity = new()
                {
                    Name = string.Empty,
                    Surname = string.Empty,
                    Firm = string.Empty
                };
            }
            catch (Exception ex)
            {
                result.Type = ResultName.Error.ToLowerString();
                result.Message = ResultMessages.Error;
            }

            return result;
        }

        /// <summary>
        /// Rehberdeki Belli Bir Kiþiyi Elde Eder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public QueryResult<PersonData> GetPerson(Guid id)
        {
            QueryResult<PersonData> result = new();

            try
            {
                Person person = _personService.GetPerson(id);
                result.Entity = _mapper.Map<PersonData>(person);
            }
            catch (Exception ex)
            {
                result.Type = ResultName.Error.ToLowerString();
                result.Message = ResultMessages.Error;
            }

            return result;
        }

        /// <summary>
        /// Rehbere Belli Bir Kiþiyi Kaydeder
        /// </summary>
        /// <param name="personData"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Save(PersonData personData)
        {
            Result result = new();

            // Zorunlu Alan Kontrolleri
            if (string.IsNullOrEmpty(personData.Name) || string.IsNullOrEmpty(personData.Surname) || string.IsNullOrEmpty(personData.Firm))
            {
                result.Type = ResultName.Warning.ToLowerString();
                result.Message = ResultMessages.InputEmpty;
                return result;
            }

            try
            {
                Person person = _mapper.Map<Person>(personData);
                result = _personService.Save(person);
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
                result = _personService.Delete(ids);
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