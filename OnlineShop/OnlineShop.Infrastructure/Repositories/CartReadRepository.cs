using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.Carts.ReadModels;
using OnlineShop.Infrastructure.CommonSql;

namespace OnlineShop.Infrastructure.Repositories
{
    public sealed class CartReadRepository : ICartReadRepository
    {
        private readonly string _connectionString;

        public CartReadRepository(DatabaseConnectionString connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public Task<List<CartReadModel>> GetCartsAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}