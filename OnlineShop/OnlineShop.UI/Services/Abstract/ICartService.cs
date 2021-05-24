using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OnlineShop.UI.Models.Cart;

namespace OnlineShop.UI.Services.Abstract
{
    public interface ICartService
    {
        Task<string> AddCartAsync(AddCartRequest addCartRequest, string token, CancellationToken cancellationToken);

        Task<string> DeleteCartAsync(long cartId, string token, CancellationToken cancellationToken);

        Task<string> UpdateCartAsync(UpdateCartRequest updateCartRequest, string token, CancellationToken cancellationToken);

        Task<List<CartViewModel>> GetCartsAsync(string userId, string token, CancellationToken cancellationToken);
    }
}