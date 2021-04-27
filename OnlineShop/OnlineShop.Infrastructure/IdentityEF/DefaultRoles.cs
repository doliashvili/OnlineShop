using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.CommonModels.Identity;
using OnlineShop.Infrastructure.Constants;

namespace OnlineShop.Infrastructure.IdentityEF
{
    public static class DefaultRoles
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            //Seed Roles

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = IdentityDbConstants.AdminRoleId,
                    Name = nameof(Roles.Admin),
                    NormalizedName = "ADMIN"
                });

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = IdentityDbConstants.ModeratorRoleId,
                    Name = nameof(Roles.Moderator),
                    NormalizedName = "MODERATOR"
                });

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = IdentityDbConstants.UserRoleId,
                    Name = nameof(Roles.User),
                    NormalizedName = "USER"
                });

        }
    }
}