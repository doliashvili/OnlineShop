using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.UI.Models.Cart;
using OnlineShop.UI.Services.Abstract;

namespace OnlineShop.UI.Services.Implements
{
    public class CartService : ICartService
    {
        public Task<string> AddCartAsync(AddCartRequest addCartRequest)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteCartAsync(long cartId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CartViewModel>> GetCartsAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}