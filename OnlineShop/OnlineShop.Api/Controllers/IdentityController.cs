using System.Threading.Tasks;
using ApiCommon.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services.Abstract;
using OnlineShop.Domain.CommonModels.Identity;

namespace OnlineShop.Api.Controllers
{
    [Route("api/Account")]
    public class IdentityController : BaseApiController
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        /// <summary>
        /// Generates a JSON Web Token for a valid combination of emailId and password.
        /// </summary>
        /// <param name="tokenRequest"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTokenAsync(TokenRequest tokenRequest)
        {
            var ipAddress = RemoteIpV4;
            var response = await _identityService.GetTokenAsync(tokenRequest, ipAddress).ConfigureAwait(false);
            if (response.Result.Succeeded)
                return Ok(response.Token);

            return StatusCode(response.Result.IsLockedOut
                ? StatusCodes.Status403Forbidden
                : StatusCodes.Status406NotAcceptable);
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var ipAddress = RemoteIpV4;
            var token = await _identityService.RefreshTokenAsync(request, ipAddress).ConfigureAwait(false);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _identityService.RegisterAsync(request, origin).ConfigureAwait(false));
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
        {
            return Ok(await _identityService.ConfirmEmailAsync(userId, code).ConfigureAwait(false));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequest model)
        {
            await _identityService.ForgotPasswordAsync(model, Request.Headers["origin"]).ConfigureAwait(false);
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest model)
        {
            return Ok(await _identityService.ResetPasswordAsync(model).ConfigureAwait(false));
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordRequest request)
        {
            request.UserId = CurrentUserId;
            return Ok(await _identityService.ChangePasswordAsync(request).ConfigureAwait(false));
        }
    }
}