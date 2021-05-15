using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OnlineShop.Infrastructure.CommonSql;

namespace OnlineShop.Infrastructure.Jobs
{
    public class ProductDeleteJob
    {
        private readonly string _connectionString;

        public ProductDeleteJob(DatabaseConnectionString connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public async Task ExecuteAsync()
        {
            string sql = @"DELETE FROM Products
            WHERE IsDeleted = 1 AND CreateTime <= @datetime";
            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@datetime", SqlDbType.DateTime).Value = DateTime.Now.AddDays(-6);

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }
    }
}