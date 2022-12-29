using Lamar;
using ContactService.Service.Test.DI;
using RiseTechDemoApp.Domain.DBModels;
using RiseTechDemoApp.Domain.DTO;

namespace ContactService.Service.Test
{
    public class ContactInfoServiceTest : IClassFixture<LamarContainerFactory>
    {
        readonly IContainer _container;
        readonly IContactInfoService _contactInfoService;

        public ContactInfoServiceTest(LamarContainerFactory factory)
        {
            _container = factory.Container;
            _contactInfoService = _container.GetInstance<IContactInfoService>();
        }

        [Fact]
        public void GetList()
        {
            QueryParams<ContactInfoData> queryParams = new() { Filter = new() };
            _contactInfoService.GetList(queryParams);
        }

        [Fact]
        public void GetContactInfo()
        {
            _contactInfoService.GetContactInfo(Guid.NewGuid());
        }

        [Fact]
        public void GetContactInfoWithPerson()
        {
            _contactInfoService.GetContactInfoWithPerson(Guid.NewGuid());
        }

        [Fact]
        public void GetReportData()
        {
            _contactInfoService.GetReportData();
        }

        [Fact]
        public void Save()
        {
            _contactInfoService.Save(new ContactInfo() { PersonId = Guid.Parse("5fcc5c4a-bb09-49d1-a27c-73f4047aa742") });
        }

        [Fact]
        public void Delete()
        {
            _contactInfoService.Delete(new List<Guid>());
        }
    }
}