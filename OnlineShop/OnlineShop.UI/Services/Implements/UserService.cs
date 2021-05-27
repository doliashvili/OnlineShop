using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using OnlineShop.UI.Models.User;
using OnlineShop.UI.Services.Abstract;
using OnlineShop.UI.Extensions;
using OnlineShop.UI.Helpers;

namespace OnlineShop.UI.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public UserService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClientFactory.CreateClient("OnlineShop");
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            var response = await _httpClient.SendAsync<LoginResponse>(HttpMethod.Post, "Login", loginRequest.AsJson(),
                CancellationToken.None);

            var token = response.Token.TokenValue;

            await _localStorage.SetItemAsync("authToken", token.AsJson());
            await _localStorage.SetItemAsync("userId", response.UserId);
            await _localStorage.SetItemAsync("loginResult", response.Result.AsJson());

            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginRequest.Email);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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

        public async Task LogOutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");

            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();

            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}