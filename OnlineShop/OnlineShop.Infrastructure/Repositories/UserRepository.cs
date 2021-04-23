using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.CommonModels.Identity;
using OnlineShop.Infrastructure.CommonSql;

namespace OnlineShop.Infrastructure.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(DatabaseConnectionString connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public Task UpdateRefreshTokenAsync(string email,RefreshToken refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser?> FindUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
