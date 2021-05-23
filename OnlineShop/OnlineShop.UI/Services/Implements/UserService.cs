using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using OnlineShop.UI.Models.User;
using OnlineShop.UI.Services.Abstract;
using OnlineShop.UI.Extensions;

namespace OnlineShop.UI.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            var response = await _httpClient.SendAsync<LoginResponse>(HttpMethod.Post, "Login", loginRequest.AsJson(),
                CancellationToken.None);

            return response;
        }

        public async Task<RegisterResponse> RegistrationAsync(RegisterRequest registerRequest)
        {
            var response = await _httpClient.SendAsync<RegisterResponse>(HttpMethod.Post, "register",
                registerRequest.AsJson(), CancellationToken.None);

            return response;
        }

        public async Task<string> ForgetPasswordAsync(ForgotPasswordRequest forgotPasswordRequest)
        {
            var response = await _httpClient.SendAsync<string>(HttpMethod.Post, "forgot-password",
                forgotPasswordRequest.AsJson(), CancellationToken.None);

            return response;
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest)
        {
            var response = await _httpClient.SendAsync<string>(HttpMethod.Post, "reset-password",
                resetPasswordRequest.AsJson(), CancellationToken.None);

            return response;
        }

        public async Task<string> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest)
        {
            var response = await _httpClient.SendAsync<string>(HttpMethod.Post, "change-password",
                changePasswordRequest.AsJson(), CancellationToken.None);

            return response;
        }
    }
}