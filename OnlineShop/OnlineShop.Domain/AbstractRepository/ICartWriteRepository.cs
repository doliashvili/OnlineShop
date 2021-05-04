using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Domain.Carts.DomainObjects;

namespace OnlineShop.Domain.AbstractRepository
{
    public interface ICartWriteRepository
    {
        Task AddCartAsync(Cart cart, string userId);

        Task RemoveCartAsync(long id);

        Task UpdateCartQuantityAsync(long id, long productId, byte quantity);
    }
}