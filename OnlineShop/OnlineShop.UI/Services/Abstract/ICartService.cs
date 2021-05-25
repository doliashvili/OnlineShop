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
        Task<Result> AddCartAsync(AddCartRequest addCartRequest, CancellationToken cancellationToken);

        Task<Result> DeleteCartAsync(long cartId, CancellationToken cancellationToken);

        Task<Result> UpdateCartAsync(UpdateCartRequest updateCartRequest, CancellationToken cancellationToken);

        Task<List<CartViewModel>> GetCartsAsync(string userId, CancellationToken cancellationToken);

        Task<int> GetCartsCountAsync(string userId, CancellationToken cancellationToken);
    }
}