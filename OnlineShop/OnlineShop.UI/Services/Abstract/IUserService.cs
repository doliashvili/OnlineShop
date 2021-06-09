using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.UI.Models.User;
using OnlineShop.UI.Models.Common;

namespace OnlineShop.UI.Services.Abstract
{
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);

        Task LogOutAsync();

        Task<RegisterResponse> RegistrationAsync(RegisterRequest registerRequest);

        Task<Models.Common.Result<string>> ForgetPasswordAsync(ForgotPasswordRequest forgotPasswordRequest);

        Task<Models.Common.Result<string>> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest);

        Task<Models.Common.Result<string>> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest);
    }
}