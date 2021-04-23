using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace ApiCommon.BaseControllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected IMediator Mediator => (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));
        protected string CurrentUserId => GetUserId();
        public string RemoteIpV4 => HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

        protected string GetUserId()
        {
            var authorization = Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrWhiteSpace(authorization) && authorization.StartsWith("Bearer "))
            {
                string token = authorization.Split(" ").LastOrDefault();

                if (!string.IsNullOrWhiteSpace(token))
                {
                    var jwtToken = new JwtSecurityToken(token);
                    return jwtToken.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub).Select(x => x.Value).FirstOrDefault();
                }
            }

            return default;
        }
    }
}