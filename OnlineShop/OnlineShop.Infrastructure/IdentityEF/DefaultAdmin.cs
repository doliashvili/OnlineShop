using System;
using System.Runtime.CompilerServices;
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
                UserName = "admin@gmail.com",
                NormalizedUserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                FirstName = "levan",
                LastName = "doliashvili",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true,
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "doliashvili"),
                SecurityStamp = string.Empty,
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