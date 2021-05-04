using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.Carts.ReadModels;

namespace OnlineShop.Infrastructure.Repositories
{
    public sealed class CartReadRepository : ICartReadRepository
    {
        public Task<List<CartReadModel>> GetCartsAsync(List<long> productIds, string userId)
        {
            throw new NotImplementedException();
        }
    }
}