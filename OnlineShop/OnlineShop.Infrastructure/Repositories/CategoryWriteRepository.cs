using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.Categories.DomainObjects;
using OnlineShop.Domain.Categories.ReadModels;
using OnlineShop.Infrastructure.CommonSql;
using OnlineShop.Infrastructure.Constants;

namespace OnlineShop.Infrastructure.Repositories
{
    public class CategoryWriteRepository : ICategoryWriteRepository
    {
        private readonly string _connectionString;

        public CategoryWriteRepository(DatabaseConnectionString connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public async Task AddCategoryAsync(Category category)
        {
            const string sql =
@"IF NOT EXISTS (SELECT ProductType FROM Categories WHERE ProductType = @productType)
INSERT INTO [Categories] ([Id],[ProductType])
VALUES (@id,@productType)";

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@id", SqlDbType.BigInt).Value = category.Id;
            command.Parameters.Add("@productType", SqlDbType.NVarChar, 50).Value = category.ProductType;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task DeleteCategoryAsync(long id)
        {
            const string sql = "DELETE FROM [Categories] WHERE Id=@id";

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }
    }
}