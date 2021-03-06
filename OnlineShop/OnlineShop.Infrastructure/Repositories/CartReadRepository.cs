using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.Carts.ReadModels;
using OnlineShop.Infrastructure.CommonSql;
using OnlineShop.Infrastructure.Constants;

namespace OnlineShop.Infrastructure.Repositories
{
    public sealed class CartReadRepository : ICartReadRepository
    {
        private readonly string _connectionString;

        public CartReadRepository(DatabaseConnectionString connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public async Task<int> GetCartsCountAsync(string userId)
        {
            const string sql = "SELECT Count(Id) FROM Carts WHERE UserId = @userId";

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@userId", SqlDbType.NVarChar, IdentityDbConstants.UserIdLength).Value = userId;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);
            if (!await reader.ReadAsync().ConfigureAwait(false))
            {
                return 0;
            }

            var count = reader.AsInt32(0);
            return count;
        }

        public async Task<List<CartReadModel>> GetCartsAsync(string userId) //TODO დროებითია ეს ქვერები ოპტიმიზაციას საჭიროებს
        {
            const string sql =
  @"SELECT dbo.Products.Id,Price,[Name],DiscountPrice,Images.[Url],c.Id,c.Quantity
      FROM Products
      LEFT JOIN Carts c ON Products.Id=c.ProductId
	  LEFT JOIN Images ON Products.Id=Images.ProductId
	  WHERE Images.MainImage=1 AND c.UserId=@userId";

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@userId", SqlDbType.NVarChar, IdentityDbConstants.UserIdLength).Value = userId;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

            var cartModels = new List<CartReadModel>(10);

            while (await reader.ReadAsync().ConfigureAwait(false))
            {
                var cart = ReadCartReadModel(reader, userId);
                cartModels.Add(cart);
            }

            return cartModels;
        }

        public static CartReadModel ReadCartReadModel(SqlDataReader reader, string userId)
        {
            var idx = 0;
            var productId = reader.AsInt64(idx++);
            var price = reader.AsDecimal(idx++);
            var name = reader.AsString(idx++);
            var discountPrice = reader.AsDecimalOrNull(idx++);
            var imageUrl = reader.AsString(idx++);
            var id = reader.AsInt64(idx++);
            var quantity = reader.AsInt16(idx);

            var cartReadModel = new CartReadModel()
            {
                ProductId = productId,
                Price = price,
                Name = name,
                DiscountPrice = discountPrice,
                Quantity = quantity,
                ImageUrl = imageUrl,
                UserId = userId,
                Id = id
            };
            return cartReadModel;
        }
    }
}