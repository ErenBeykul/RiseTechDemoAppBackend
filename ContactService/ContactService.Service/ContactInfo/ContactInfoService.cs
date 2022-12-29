using ContactService.DataAccess;
using Microsoft.EntityFrameworkCore;
using RiseTechDemoApp.Domain.DBModels;
using RiseTechDemoApp.Domain.DTO;
using RiseTechDemoApp.Domain.Constants;
using RiseTechDemoApp.Domain.Enums;
using RiseTechDemoApp.Domain.Extensions;

namespace ContactService.Service
{
    public class ContactInfoService : IContactInfoService
    {
        readonly IRiseTechDemoAppContactContext _context;

        public ContactInfoService(IRiseTechDemoAppContactContext context)
        {
            _context = context;
        }

        public QueryData<ContactInfoData> GetList(QueryParams<ContactInfoData> queryParams)
        {
            List<ContactInfoData> contactInfo = new();
            var dataContactInfo = _context.ContactInfo.Where(x => x.PersonId == queryParams.Filter.PersonId && !x.IsDeleted);
            if (!string.IsNullOrEmpty(queryParams.Filter.InfoType)) dataContactInfo = dataContactInfo.Where(x => x.InfoType == queryParams.Filter.InfoType.GetDisplayName<InfoType>());
            if (!string.IsNullOrEmpty(queryParams.Filter.Info)) dataContactInfo = dataContactInfo.Where(x => x.Info.ToLower().Contains(queryParams.Filter.Info.ToLower()));

            if (!string.IsNullOrEmpty(queryParams.SortField))
            {
                if (queryParams.SortOrder == "asc")
                {
                    if (queryParams.SortField == "infoType") dataContactInfo = dataContactInfo.OrderBy(x => x.InfoType);
                    else if (queryParams.SortField == "info") dataContactInfo = dataContactInfo.OrderBy(x => x.Info);
                }
                else if (queryParams.SortOrder == "desc")
                {
                    if (queryParams.SortField == "infoType") dataContactInfo = dataContactInfo.OrderByDescending(x => x.InfoType);
                    else if (queryParams.SortField == "info") dataContactInfo = dataContactInfo.OrderByDescending(x => x.Info);
                }
            }

            var pagedContactInfo = dataContactInfo.Skip((queryParams.PageNumber - 1) * queryParams.PageSize).Take(queryParams.PageSize);

            foreach (var item in pagedContactInfo)
            {
                ContactInfoData info = new()
                {
                    Id = item.Id,
                    InfoType = item.InfoType,
                    Info = item.Info
                };

                contactInfo.Add(info);
            }

            QueryData<ContactInfoData> queryData = new()
            {
                TotalCount = dataContactInfo.Count(),
                Entities = contactInfo
            };

            return queryData;
        }

        public ContactInfo GetContactInfo(Guid id)
        {
            return _context.ContactInfo.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        }

        public ContactInfo GetContactInfoWithPerson(Guid id)
        {
            return _context.ContactInfo.Include(x => x.Person).FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        }

        public List<ReportData> GetReportData()
        {
            List<ReportData> reportData = new();
            var contactInfo = _context.ContactInfo.Where(x => x.InfoType == InfoType.Location.GetDisplayName() && !x.IsDeleted)
                                                    .Select(x => new { x.PersonId, x.Info })
                                                    .ToList();

            var locations = contactInfo.Select(x => x.Info).Distinct();
            var personIds = contactInfo.Select(x => x.PersonId).Distinct();

            var personCountInfo = contactInfo.GroupBy(x => x.Info).Select(x => new { Location = x.Key, PersonCount = x.Count() }).ToList();

            var phoneNumberCountInfo = _context.ContactInfo.Where(x => x.InfoType == InfoType.Phone.GetDisplayName() &&
                                                                        personIds.Contains(x.PersonId) && !x.IsDeleted)
                                                            .GroupBy(x => x.PersonId)
                                                            .Select(x => new { PersonId = x.Key, PhoneNumberCount = x.Count() })
                                                            .ToList();

            foreach (var item in locations)
            {
                var personIdsInThatLocation = contactInfo.Where(x => x.Info == item).Select(x => x.PersonId);

                ReportData data = new()
                {
                    Location = item,
                    PersonCount = personCountInfo.Where(x => x.Location == item).Select(x => x.PersonCount).FirstOrDefault(),
                    PhoneNumberCount = phoneNumberCountInfo.Where(x => personIdsInThatLocation.Contains(x.PersonId))
                                                            .Select(x => x.PhoneNumberCount)
                                                            .Sum()
                };

                reportData.Add(data);
            }

            return reportData;
        }

        public Result Save(ContactInfo info)
        {
            Result result = new();

            if (info.Id == Guid.Empty)
            {
                #region Ekleme İşlemleri
                info.Id = Guid.NewGuid();
                info.CreateDate = DateTime.Now;

                _context.ContactInfo.Add(info);
                #endregion
            }
            else
            {
                #region Düzenleme İşlemleri
                ContactInfo dataInfo = GetContactInfo(info.Id);

                if (dataInfo == null)
                {
                    result.Type = ResultName.Warning.ToLowerString();
                    result.Message = ResultMessages.NonExistingData;

                    return result;
                }

                dataInfo.InfoType = info.InfoType;
                dataInfo.Info = info.Info;
                dataInfo.UpdateDate = DateTime.Now;

                _context.ContactInfo.Update(dataInfo);
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
            var dataInfo = _context.ContactInfo.Where(x => ids.Contains(x.Id) && !x.IsDeleted);
            foreach (var item in dataInfo) item.IsDeleted = true;

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