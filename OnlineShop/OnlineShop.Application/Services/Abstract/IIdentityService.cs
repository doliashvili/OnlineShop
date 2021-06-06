using System.Threading.Tasks;
using Helpers.ReadResults;
using OnlineShop.Domain.CommonModels.Identity;

namespace OnlineShop.Application.Services.Abstract
{
    public interface IIdentityService
    {
        Task<AuthResult> GetTokenAsync(TokenRequest request, string ipAddress);

        Task<Result<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress);

        Task<Result<string>> RegisterAsync(RegisterRequest request, string origin);

        Task<Result<string>> ConfirmEmailAsync(string userId, string code);

        Task ForgotPasswordAsync(ForgotPasswordRequest model, string origin);

        Task<Result<string>> ResetPasswordAsync(ResetPasswordRequest model);

        Task<IResult> ChangePasswordAsync(ChangePasswordRequest request);

        Task<IResult> AddPersonalInfoAsync(AddPersonalInfoRequest request);
    }
}