using AutoMapper;

namespace ContactService.UI.Management
{
    public class ControllerManager : IControllerManager
    {
        public IMapper Mapper { get; }

        public ControllerManager(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}