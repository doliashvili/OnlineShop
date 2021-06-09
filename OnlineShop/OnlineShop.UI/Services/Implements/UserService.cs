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
using System.IdentityModel.Tokens.Jwt;

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
            var response = await _httpClient.SendAsync<LoginResponse>(HttpMethod.Post, "api/Account/Login", loginRequest.AsJson(),
                CancellationToken.None);

            var token = response.Token.Token;

            var jwtToken = new JwtSecurityToken(token);
            var firstName = jwtToken.Claims.Where(x => x.Type == "first_name").Select(x => x.Value).FirstOrDefault();
            var lastName = jwtToken.Claims.Where(x => x.Type == "last_name").Select(x => x.Value).FirstOrDefault();

            await _localStorage.SetItemAsync("firstName", firstName);
            await _localStorage.SetItemAsync("lastName", lastName);
            await _localStorage.SetItemAsync("authToken", token);
            await _localStorage.SetItemAsync("token", response.Token.AsJson());
            await _localStorage.SetItemAsync("userId", response.UserId);
            await _localStorage.SetItemAsync("loginResult", response.Result.AsJson());

            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginRequest.Email);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return response;
        }

        public async Task<RegisterResponse> RegistrationAsync(RegisterRequest registerRequest)
        {
            var headers = new HeaderCollection();

            headers.Add("origin", _httpClient.BaseAddress?.OriginalString);
            headers.CopyTo(_httpClient.DefaultRequestHeaders);

            var response = await _httpClient.SendAsync<RegisterResponse>(HttpMethod.Post, "api/Account/register",
                registerRequest.AsJson(), CancellationToken.None);

            return response;
        }

        public async Task<Models.Common.Result<string>> ForgetPasswordAsync(ForgotPasswordRequest forgotPasswordRequest)
        {
            var response = await _httpClient.SendAsync<Models.Common.Result<string>>(HttpMethod.Post, "api/Account/forgot-password",
                forgotPasswordRequest.AsJson(), CancellationToken.None);

            return response;
        }

        public async Task<Models.Common.Result<string>> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest)
        {
            var response = await _httpClient.SendAsync<Models.Common.Result<string>>(HttpMethod.Post, "api/Account/reset-password",
                resetPasswordRequest.AsJson(), CancellationToken.None);

            return response;
        }

        public async Task<Models.Common.Result<string>> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest)
        {
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrWhiteSpace(savedToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
            }

            // changePasswordRequest.UserId = await _localStorage.GetItemAsync<string>("userId");

            var response = await _httpClient.SendAsync<Models.Common.Result<string>>(HttpMethod.Put, "api/Account/change-password",
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