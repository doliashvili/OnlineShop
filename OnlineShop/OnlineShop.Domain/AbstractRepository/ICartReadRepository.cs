using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Domain.Carts.ReadModels;

namespace OnlineShop.Domain.AbstractRepository
{
    public interface ICartReadRepository
    {
        Task<List<CartReadModel>> GetCartsAsync(string userId);
    }
}