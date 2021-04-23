using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.CommonModels.Identity;
using OnlineShop.Domain.Extensions;
using OnlineShop.Infrastructure.CommonSql;

namespace OnlineShop.Infrastructure.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(DatabaseConnectionString connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public async Task UpdateRefreshTokenAsync(string email, RefreshToken refreshToken)
        {
            const string sql =
 @"UPDATE User
SET RefreshToken=@refreshToken
WHERE Email=@email;";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
            command.Parameters.Add("@RefreshToken", SqlDbType.NVarChar).Value = refreshToken.AsJson();

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task<ApplicationUser?> FindUserByEmailAsync(string email)
        {
            const string sql =
 @"SELECT [FirstName],[LastName],[PersonalNumber],[Country],[City],[Address],[IdentificationNumber],[Email],[Created],[DateOfBirth],[RefreshToken]
FROM Users
WHERE Email=@email;";
            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

            if (!await reader.ReadAsync().ConfigureAwait(false))
            {
                return null;
            }

            return ReadApplicationUser(reader);
        }

        private static ApplicationUser ReadApplicationUser(SqlDataReader reader)
        {
            var idx = 0;

            var firstName = reader.AsStringOrNull(idx++);
            var lastName = reader.AsStringOrNull(idx++);
            var personalNumber = reader.AsStringOrNull(idx++);
            var country = reader.AsStringOrNull(idx++);
            var city = reader.AsStringOrNull(idx++);
            var address = reader.AsStringOrNull(idx++);
            var identificationNumber = reader.AsStringOrNull(idx++);
            var email = reader.AsString(idx++);
            var cratedAt = reader.AsDateTime(idx++);
            var dateOfBirth = reader.AsDateTimeOrNull(idx++);
            var refreshToken = reader.AsJson<RefreshToken>(idx);

            return new ApplicationUser()
            {
                FirstName = firstName,
                LastName = lastName,
                PersonalNumber = personalNumber,
                Country = country,
                City = city,
                Address = address,
                IdentificationNumber = identificationNumber,
                Email = email,
                CreatedAt = cratedAt,
                DateOfBirth = dateOfBirth,
                RefreshToken = refreshToken
            };
        }
    }
}
