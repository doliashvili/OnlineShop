using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using OnlineShop.UI.Extensions;
using OnlineShop.UI.Helpers;
using OnlineShop.UI.Models.Cart;
using OnlineShop.UI.Models.Product;
using OnlineShop.UI.Services.Abstract;

namespace OnlineShop.UI.Services.Implements
{
    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AddCartAsync(AddCartRequest addCartRequest, string token, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Add("Bearer", token);

            var response = await _httpClient.SendAsync<string>(HttpMethod.Post, "v1/Cart/AddCart", addCartRequest.AsJson(), cancellationToken);
            return response;
        }

        public async Task<string> DeleteCartAsync(long cartId, string token, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Add("Bearer", token);

            var response = await _httpClient.SendAsync<string>(HttpMethod.Delete, "v1/Cart/DeleteCart", new { Id = cartId }.AsJson(), cancellationToken);
            return response;
        }

        public async Task<string> UpdateCartAsync(UpdateCartRequest updateCartRequest, string token, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Add("Bearer", token);

            var response = await _httpClient.SendAsync<string>(HttpMethod.Post, "v1/Cart/GetAllCarts", updateCartRequest.AsJson(), cancellationToken);
            return response;
        }

        public async Task<List<CartViewModel>> GetCartsAsync(string userId, string token, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Add("Bearer", token);

            var response = await _httpClient.SendAsync<List<CartViewModel>>(HttpMethod.Get, "v1/Cart/GetAllCarts", new { Id = userId }.AsJson(), cancellationToken);
            return response;
        }
    }
}