using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OnlineShop.Application.Services.Abstract;
using OnlineShop.Domain.CommonModels.Carts;
using OnlineShop.Domain.Extensions;
using StackExchange.Redis;

namespace OnlineShop.Application.Services.Implements
{
    public class CartService : ICartService
    {
        private readonly IConnectionMultiplexer _multiplexer;

        public CartService(IConnectionMultiplexer multiplexer)
        {
            _multiplexer = multiplexer;
        }

        public Task<bool> AddCartAsync(CartGuest cart, TimeSpan expire)
        {
            if (cart is null)
            {
                throw new ArgumentNullException();
            }

            var db = _multiplexer.GetDatabase(0);
            return db.StringSetAsync(cart.Ip, cart.AsJson(), expire);
        }

        public Task<bool> RemoveAsync(string key)
        {
            var db = _multiplexer.GetDatabase(0);
            return db.KeyDeleteAsync(key);
        }

        public async Task<CartGuest> GetCartAsync(string key)
        {
            var db = _multiplexer.GetDatabase(0);
            var result = await db.StringGetAsync(key).ConfigureAwait(false);
            if (result.IsNull)
            {
                return null;
            }

            return JsonSerializer.Deserialize<CartGuest>(result);
        }
    }
}