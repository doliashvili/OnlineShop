using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using OnlineShop.UI.Extensions;
using OnlineShop.UI.Models.Cart;
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

        public async Task<string> AddCartAsync(AddCartRequest addCartRequest, CancellationToken cancellationToken)
        {
            var response = await _httpClient.SendAsync<string>(HttpMethod.Post, "v1/Cart/AddCart", addCartRequest.AsJson(), cancellationToken);
            return response;
        }

        public async Task<string> DeleteCartAsync(long cartId, CancellationToken cancellationToken)
        {
            var response = await _httpClient.SendAsync<string>(HttpMethod.Delete, "v1/Cart/DeleteCart", new { Id = cartId }.AsJson(), cancellationToken);
            return response;
        }

        public async Task<string> UpdateCartAsync(UpdateCartRequest updateCartRequest, CancellationToken cancellationToken)
        {
            var response = await _httpClient.SendAsync<string>(HttpMethod.Post, "v1/Cart/GetAllCarts", updateCartRequest.AsJson(), cancellationToken);
            return response;
        }

        public async Task<List<CartViewModel>> GetCartsAsync(string userId, CancellationToken cancellationToken)
        {
            var response = await _httpClient.SendAsync<List<CartViewModel>>(HttpMethod.Get, "v1/Cart/GetAllCarts", new { Id = userId }.AsJson(), cancellationToken);
            return response;
        }
    }
}