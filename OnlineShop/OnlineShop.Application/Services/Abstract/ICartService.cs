using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Domain.CommonModels.Carts;

namespace OnlineShop.Application.Services.Abstract
{
    public interface ICartService
    {
        Task<bool> AddCartAsync(Cart cart, TimeSpan expire);

        Task<bool> RemoveAsync(string key);

        Task<Cart> GetCartAsync(string key);
    }
}