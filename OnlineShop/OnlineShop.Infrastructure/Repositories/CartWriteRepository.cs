using System;
using System.Data;
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

        public async Task AddCartAsync(Cart cart)
        {
            string sql =
 @"IF(@quantity <= (SELECT Quantity FROM Products WHERE Id = @productId))
INSERT INTO Carts (Id,ProductId,UserId,Quantity)
VALUES (@id,@productId,@userId,@quantity)";

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@id", SqlDbType.BigInt).Value = cart.Id;
            command.Parameters.Add("@productId", SqlDbType.BigInt).Value = cart.ProductId;
            command.Parameters.Add("@userId", SqlDbType.NVarChar, IdentityDbConstants.UserIdLength).Value = cart.UserId;
            command.Parameters.Add("@quantity", SqlDbType.TinyInt).Value = cart.Quantity;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task RemoveCartAsync(long id)
        {
            const string sql = "DELETE Carts WHERE Id=@id;";

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task UpdateCartQuantityAsync(long id, long productId, byte quantity)
        {
            const string sql =
  @"IF(@quantity <= (SELECT Quantity FROM Products WHERE Id = @productId))
UPDATE Carts
SET Quantity=@quantity
WHERE Id=@id";

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
            command.Parameters.Add("@productId", SqlDbType.BigInt).Value = productId;
            command.Parameters.Add("@quantity", SqlDbType.TinyInt).Value = quantity;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }
    }
}