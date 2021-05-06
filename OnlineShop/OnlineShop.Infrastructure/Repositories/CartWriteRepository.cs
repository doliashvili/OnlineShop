using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.Carts.DomainObjects;
using OnlineShop.Infrastructure.CommonSql;
using OnlineShop.Infrastructure.Constants;

namespace OnlineShop.Infrastructure.Repositories
{
    public sealed class CartWriteRepository : ICartWriteRepository
    {
        private readonly string _connectionString;

        public CartWriteRepository(DatabaseConnectionString connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public async Task AddCartAsync(Cart cart, string userId)
        {
            const string sql =
 @"BEGIN TRAN;
 INSERT INTO Carts (Id,ProductId,DiscountPrice,Name,Price,Quantity,ImageUrl)
VALUES (@id,@productId,@discountPrice,@name,@price,@quantity,@imageUrl);

 INSERT INTO UsersCarts (UserId,CartId)
VALUES (@userId,@id);
COMMIT TRAN;";

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@id", SqlDbType.BigInt).Value = cart.Id;
            command.Parameters.Add("@productId", SqlDbType.BigInt).Value = cart.ProductId;
            command.Parameters.Add("@discountPrice", SqlDbType.SmallMoney).SetValue(cart.DiscountPrice);
            command.Parameters.Add("@name", SqlDbType.NVarChar, ProductDbConstants.CartName).Value = cart.Name;
            command.Parameters.Add("@price", SqlDbType.SmallMoney).Value = cart.Price;
            command.Parameters.Add("@quantity", SqlDbType.TinyInt).Value = cart.Quantity;
            command.Parameters.Add("@imageUrl", SqlDbType.VarChar, ProductDbConstants.Url).Value = cart.ImageUrl;
            command.Parameters.Add("@userId", SqlDbType.NVarChar, IdentityDbConstants.UserIdLength).Value = userId;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task RemoveCartAsync(long id)
        {
            const string sql =
@"BEGIN TRAN;
  DELETE Carts WHERE Id=@id;

  DELETE UsersCarts CartId=@id;
 COMMIT TRAN;";

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public Task UpdateCartQuantityAsync(long id, long productId, byte quantity)
        {
            throw new NotImplementedException();
        }
    }
}