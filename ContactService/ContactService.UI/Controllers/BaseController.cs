using AutoMapper;
using ContactService.UI.Attributes;
using ContactService.UI.Management;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ContactService.UI.Controllers
{
    [ServiceFilter(typeof(AuthAttribute))]
    public class BaseController : ControllerBase
    {
        public IControllerManager ControllerManager { get; set; }
        protected IMapper Mapper => ControllerManager.Mapper;
        protected JsonSerializerOptions SerializerOptions => new()
        {
            PropertyNameCaseInsensitive = true
        };
    }
}