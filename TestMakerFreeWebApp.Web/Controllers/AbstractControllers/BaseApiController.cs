using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TestMakerFreeWebApp.Web.Controllers.AbstractControllers
{
    [Route("api/[controller]")]
    public abstract class BaseApiController : Controller
    {
        protected IMapper Mapper { get; }

        protected JsonSerializerSettings JsonSettings { get; }

        public BaseApiController(IMapper mapper)
        {
            Mapper = mapper;
            JsonSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
        }
    }
}
