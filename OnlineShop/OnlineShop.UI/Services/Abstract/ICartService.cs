using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OnlineShop.UI.Models.Cart;
using OnlineShop.UI.Models.Common;

namespace OnlineShop.UI.Services.Abstract
{
    public interface ICartService
    {
        Task<bool> AddCartAsync(AddCartRequest addCartRequest, CancellationToken cancellationToken);

        Task<bool> DeleteCartAsync(long cartId, CancellationToken cancellationToken);

        Task<bool> UpdateCartAsync(UpdateCartRequest updateCartRequest, CancellationToken cancellationToken);

        Task<List<CartViewModel>> GetCartsAsync(string userId, CancellationToken cancellationToken);

        Task<int> GetCartsCountAsync(string userId, CancellationToken cancellationToken);
    }
}