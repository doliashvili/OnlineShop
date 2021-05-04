using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.Carts.DomainObjects;

namespace OnlineShop.Infrastructure.Repositories
{
    public sealed class CartWriteRepository : ICartWriteRepository
    {
        public Task AddCartAsync(Cart cart, string userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveCartAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCartQuantityAsync(long id, long productId, byte quantity)
        {
            throw new NotImplementedException();
        }
    }
}