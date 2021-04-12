using Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace ApiCommon.BaseControllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected IMediator Mediator => (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));
        protected string CurrentUserId => "";
        public string RemoteIpV4 => "";
    }
}