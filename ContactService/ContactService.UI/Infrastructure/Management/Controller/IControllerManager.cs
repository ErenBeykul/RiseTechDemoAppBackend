using AutoMapper;

namespace ContactService.UI.Management
{
    public interface IControllerManager
    {
        IMapper Mapper { get; }
    }
}