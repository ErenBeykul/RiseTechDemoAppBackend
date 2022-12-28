using ContactService.DataAccess;
using RiseTechDemoApp.Domain.DTO;
using RiseTechDemoApp.Domain.Constants;
using RiseTechDemoApp.Domain.Enums;
using RiseTechDemoApp.Domain.Extensions;
using RiseTechDemoApp.Domain.DBModels;

namespace ContactService.Service
{
    public class PersonService : IPersonService
    {
        readonly IRiseTechDemoAppContactContext _context;

        public PersonService(IRiseTechDemoAppContactContext context)
        {
            _context = context;
        }

        public QueryData<PersonData> GetPeople(QueryParams<PersonData> queryParams)
        {
            List<PersonData> people = new();
            IQueryable<Person> dataPeople = _context.People.Where(x => !x.IsDeleted);
            if (!string.IsNullOrEmpty(queryParams.Filter.Name)) dataPeople = dataPeople.Where(x => x.Name.ToLower().Contains(queryParams.Filter.Name.ToLower()));
            if (!string.IsNullOrEmpty(queryParams.Filter.Surname)) dataPeople = dataPeople.Where(x => x.Surname.ToLower().Contains(queryParams.Filter.Surname.ToLower()));
            if (!string.IsNullOrEmpty(queryParams.Filter.Firm)) dataPeople = dataPeople.Where(x => x.Firm.ToLower().Contains(queryParams.Filter.Firm.ToLower()));

            if (!string.IsNullOrEmpty(queryParams.SortField))
            {
                if (queryParams.SortOrder == "asc")
                {
                    if (queryParams.SortField == "name") dataPeople = dataPeople.OrderBy(x => x.Name);
                    else if (queryParams.SortField == "surname") dataPeople = dataPeople.OrderBy(x => x.Surname);
                    else if (queryParams.SortField == "firm") dataPeople = dataPeople.OrderBy(x => x.Firm);
                }
                else if (queryParams.SortOrder == "desc")
                {
                    if (queryParams.SortField == "name") dataPeople = dataPeople.OrderByDescending(x => x.Name);
                    else if (queryParams.SortField == "surname") dataPeople = dataPeople.OrderByDescending(x => x.Surname);
                    else if (queryParams.SortField == "firm") dataPeople = dataPeople.OrderByDescending(x => x.Firm);
                }
            }

            var pagedPeople = dataPeople.Skip((queryParams.PageNumber - 1) * queryParams.PageSize).Take(queryParams.PageSize);

            foreach (var item in pagedPeople)
            {
                PersonData person = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Surname = item.Surname,
                    Firm = item.Firm
                };

                people.Add(person);
            }

            QueryData<PersonData> queryData = new()
            {
                TotalCount = dataPeople.Count(),
                Entities = people
            };

            return queryData;
        }

        public Person GetPerson(Guid id)
        {
            return _context.People.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        }

        public Result Save(Person person)
        {
            Result result = new();

            if (person.Id == Guid.Empty)
            {
                #region Ekleme İşlemleri
                person.Id = Guid.NewGuid();
                person.CreateDate = DateTime.Now;

                _context.People.Add(person);
                #endregion
            }
            else
            {
                #region Düzenleme İşlemleri
                Person dataPerson = GetPerson(person.Id);

                if (dataPerson == null)
                {
                    result.Type = ResultName.Warning.ToLowerString();
                    result.Message = ResultMessages.NonExistingData;

                    return result;
                }

                dataPerson.Name = person.Name;
                dataPerson.Surname = person.Surname;
                dataPerson.Firm = person.Firm;
                dataPerson.UpdateDate = DateTime.Now;

                _context.People.Update(dataPerson);
                #endregion
            }

            if (_context.SaveChanges() > 0)
            {
                result.IsSuccess = true;
                result.Type = ResultName.Success.ToLowerString();
                result.Message = ResultMessages.Success;
            }
            else
            {
                result.Type = ResultName.Error.ToLowerString();
                result.Message = ResultMessages.Error;
            }

            return result;
        }

        public Result Delete(List<Guid> ids)
        {
            Result result = new();
            var dataPeople = _context.People.Where(x => ids.Contains(x.Id) && !x.IsDeleted);
            foreach (var item in dataPeople) item.IsDeleted = true;

            if (_context.SaveChanges() > 0)
            {
                result.IsSuccess = true;
                result.Type = ResultName.Success.ToLowerString();
                result.Message = ResultMessages.Success;

            }
            else
            {
                result.Type = ResultName.Error.ToLowerString();
                result.Message = ResultMessages.Error;
            }

            return result;
        }
    }
}