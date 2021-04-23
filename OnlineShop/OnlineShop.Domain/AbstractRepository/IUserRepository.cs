using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Domain.CommonModels.Identity;

namespace OnlineShop.Domain.AbstractRepository
{
    public interface IUserRepository
    {
        Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
        Task<ApplicationUser> FindUserByEmailAsync(string email);
    }
}
