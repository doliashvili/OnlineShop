using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using OnlineShop.UI.Models.NewFolder;
using OnlineShop.UI.Models.User;
using OnlineShop.UI.Services.Abstract;

namespace OnlineShop.UI.Services.Implements
{
    public class UserService : IUserService
    {
        //private readonly HttpClient _httpClient;

        //public UserService(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;
        //}

        public Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }

        public Task<RegisterResponse> RegistrationAsync(RegisterRequest registerRequest)
        {
            throw new NotImplementedException();
        }

        public Task<string> ForgetPasswordAsync(ForgotPasswordRequest forgotPasswordRequest)
        {
            throw new NotImplementedException();
        }

        public Task<string> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest)
        {
            throw new NotImplementedException();
        }

        public Task<string> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest)
        {
            throw new NotImplementedException();
        }
    }
}