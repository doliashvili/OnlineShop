using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.Categories.ReadModels;
using OnlineShop.Infrastructure.CommonSql;

namespace OnlineShop.Infrastructure.Repositories
{
    public class CategoryReadRepository : ICategoryReadRepository
    {
        private readonly string _connectionString;

        public CategoryReadRepository(DatabaseConnectionString connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public async Task<List<CategoryReadModel>> GetCategoriesAsync()
        {
            const string sql =
                "SELECT Id,ProductType FROM Categories";

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);

            await using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

            var categories = new List<CategoryReadModel>(10);

            while (await reader.ReadAsync().ConfigureAwait(false))
            {
                var cart = ReadCategoryReadModel(reader);
                categories.Add(cart);
            }

            return categories;
        }

        private static CategoryReadModel ReadCategoryReadModel(SqlDataReader reader)
        {
            var idx = 0;
            var id = reader.AsInt64(idx++);
            var productType = reader.AsString(idx);

            return new CategoryReadModel() { Category = productType, Id = id };
        }
    }
}