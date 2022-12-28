using Lamar;
using ContactService.Service.Test.DI;
using RiseTechDemoApp.Domain.DBModels;
using RiseTechDemoApp.Domain.DTO;

namespace ContactService.Service.Test
{
    public class PersonServiceTest : IClassFixture<LamarContainerFactory>
    {
        readonly IContainer _container;
        readonly IPersonService _personService;

        public PersonServiceTest(LamarContainerFactory factory)
        {
            _container = factory.Container;
            _personService = _container.GetInstance<IPersonService>();
        }

        [Fact]
        public void GetPeople()
        {
            QueryParams<PersonData> queryParams = new() { Filter = new() };
            _personService.GetPeople(queryParams);
        }

        [Fact]
        public void GetPerson()
        {
            _personService.GetPerson(Guid.NewGuid());
        }

        [Fact]
        public void Save()
        {
            _personService.Save(new Person());
        }

        [Fact]
        public void Delete()
        {
            _personService.Delete(new List<Guid>());
        }
    }
}