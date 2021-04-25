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
        Task<ApplicationUser?> FindUserByEmailAsync(string email);
        Task<ApplicationUser?> FindUserByIdAsync(string userId);
        Task UpdateRefreshTokenAsync(string email, RefreshToken refreshToken);
        Task UpdateActivatedAtAsync(string userId, DateTime dateTime);
    }
}
