using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OnlineShop.Infrastructure.CommonSql;
using OnlineShop.Infrastructure.Constants;

namespace OnlineShop.Infrastructure.Jobs
{
    public class DeleteImagesInFolder
    {
        private readonly string _connectionString;

        public DeleteImagesInFolder(DatabaseConnectionString connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public async Task ExecuteAsync(string path)
        {
            var files = Directory.GetFiles(path);

            const string sql = @"SELECT dbo.Products.Id,CreateTime,Images.Id AS ImgId,Images.ProductId,Images.Url FROM Products
            LEFT JOIN Images ON dbo.Products.Id = Images.ProductId
            WHERE IsDeleted = 1 AND Images.Url LIKE '%' + @filename AND (CreateTime <= @createTime OR CreateTime IS NULL)";

            foreach (var file in files)
            {
                try
                {
                    var fileName = file.Split(Path.DirectorySeparatorChar)[^1];

                    await using var connection = new SqlConnection(_connectionString);
                    await using var command = new SqlCommand(sql, connection);

                    command.Parameters.Add("@fileName", SqlDbType.VarChar, ProductDbConstants.Url).Value = fileName;
                    command.Parameters.Add("@createTime", SqlDbType.DateTime).Value = DateTime.Now.AddDays(-5); //todo es ufro male unda waishalos vidre producti

                    await connection.EnsureIsOpenAsync().ConfigureAwait(false);
                    await using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);
                    if (await reader.ReadAsync().ConfigureAwait(false))
                    {
                        File.Delete(file);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}