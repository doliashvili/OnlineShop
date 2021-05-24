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
        Task<string> AddCartAsync(AddCartRequest addCartRequest, CancellationToken cancellationToken);

        Task<string> DeleteCartAsync(long cartId, CancellationToken cancellationToken);

        Task<string> UpdateCartAsync(UpdateCartRequest updateCartRequest, CancellationToken cancellationToken);

        Task<List<CartViewModel>> GetCartsAsync(string userId, CancellationToken cancellationToken);
    }
}