using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.UI.Models.Cart;

namespace OnlineShop.UI.Services.Abstract
{
    public interface ICartService
    {
        Task<string> AddCartAsync(AddCartRequest addCartRequest);

        Task<string> DeleteCartAsync(long cartId);

        Task<List<CartViewModel>> GetCartsAsync(string userId);
    }
}