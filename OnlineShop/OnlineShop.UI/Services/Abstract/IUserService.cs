using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.UI.Models.NewFolder;
using OnlineShop.UI.Models.User;

namespace OnlineShop.UI.Services.Abstract
{
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);

        Task<RegisterResponse> RegistrationAsync(RegisterRequest registerRequest);

        Task<string> ForgetPasswordAsync(ForgotPasswordRequest forgotPasswordRequest);

        Task<string> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest);

        Task<string> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest);
    }
}