using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace OnlineShop.UI.Helpers
{
    public class AuthenticationHeaderHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;

        public AuthenticationHeaderHandler(ILocalStorageService localStorage)
            => _localStorage = localStorage;

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            try
            {
                if (request.Headers.Authorization?.Scheme != "Bearer")
                {
                    var savedToken = await _localStorage.GetItemAsync<string>("authToken");

                    if (!string.IsNullOrWhiteSpace(savedToken))
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}