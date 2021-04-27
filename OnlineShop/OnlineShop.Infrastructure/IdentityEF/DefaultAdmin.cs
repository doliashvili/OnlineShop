using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.CommonModels.Identity;
using OnlineShop.Domain.Crypto;
using OnlineShop.Infrastructure.Constants;

namespace OnlineShop.Infrastructure.IdentityEF
{
    public static class DefaultAdmin
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            //Seed Admin

            var defaultAdmin = new ApplicationUser
            {
                Id = IdentityDbConstants.AdminUserId,
                UserName = "admin",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                FirstName = "Levan",
                LastName = "Doliashvili",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true,
                PasswordHash = CryptoHelper.HashPassword("doliashvili"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            modelBuilder.Entity<ApplicationUser>().HasData(defaultAdmin);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = IdentityDbConstants.AdminRoleId,
                UserId = IdentityDbConstants.AdminUserId
            });
        }
    }
}