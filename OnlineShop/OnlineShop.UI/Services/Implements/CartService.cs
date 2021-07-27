using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using OnlineShop.UI.Extensions;
using OnlineShop.UI.Helpers;
using OnlineShop.UI.Models.Cart;
using OnlineShop.UI.Services.Abstract;

namespace OnlineShop.UI.Services.Implements
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;

        public CartService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            _httpClient = httpClientFactory.CreateClient("OnlineShop");
        }

        public async Task<bool> AddCartAsync(AddCartRequest addCartRequest, CancellationToken cancellationToken)
        {
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrWhiteSpace(savedToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
            }

            var response = await _httpClient.PostAsync(HttpMethod.Post, "v1/Cart/AddCart", addCartRequest.AsJson(), cancellationToken);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCartAsync(long cartId, CancellationToken cancellationToken)
        {
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrWhiteSpace(savedToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
            }

            var response = await _httpClient.PostAsync(HttpMethod.Delete, "v1/Cart/DeleteCart", new { Id = cartId }.AsJson(), cancellationToken);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCartAsync(UpdateCartRequest updateCartRequest, CancellationToken cancellationToken)
        {
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrWhiteSpace(savedToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
            }

            var response = await _httpClient.PostAsync(HttpMethod.Post, "v1/Cart/UpdateCartQuantity", updateCartRequest.AsJson(), cancellationToken);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CartViewModel>> GetCartsAsync(string userId, CancellationToken cancellationToken)
        {
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrWhiteSpace(savedToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
            }

            var qpc = new QueryParamCollection
            {
                {"userId", userId}
            }.ToQueryString();

            var response = await _httpClient.SendAsync<List<CartViewModel>>(HttpMethod.Get, $"v1/Cart/GetAllCarts?{qpc}", null, cancellationToken);
            return response;
        }

        public async Task<int> GetCartsCountAsync(string userId, CancellationToken cancellationToken)
        {
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrWhiteSpace(savedToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
            }

            var qpc = new QueryParamCollection
            {
                {"userId", userId}
            }.ToQueryString();

            var response = await _httpClient.SendAsync<int>(HttpMethod.Get, $"v1/Cart/GetCartsCount?{qpc}", null, cancellationToken);
            return response;
        }
    }
}