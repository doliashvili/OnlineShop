using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.Extensions;
using OnlineShop.Domain.Products.DomainObjects;
using OnlineShop.Infrastructure.CommonSql;
using OnlineShop.Infrastructure.Constants;

namespace OnlineShop.Infrastructure
{
    public class ProductWriteRepository : IProductWriteRepository
    {
        private readonly string _connectionString;
        public ProductWriteRepository(DatabaseConnectionString connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public async Task CreateAsync(Product product)
        {
            const string sql =
  @"INSERT INTO OnlineShop.Products (Id,Brand, Color,CreateTime,[Description],Discount,Expiration,DiscountPrice,ForBaby,Gender,IsDeleted,Name,Price,ProductType,Weight,Size)
VALUES (@id,@brand,@color,@createTime,@description,@discount,@expiration,@discountPrice,@forBaby,@gender,@isDeleted,@name,@price,@productType,@weight,@size);";

            const string sql2 =
  @"INSERT INTO OnlineShop.Images (Id,MainImage,Url,ProductId)
VALUES (@id,@mainImage,@url,@productId);";

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);
            command.Parameters.Add("@id", SqlDbType.BigInt).Value = product.Id;
            command.Parameters.Add("@brand", SqlDbType.NVarChar, ProductDbConstants.Brand).Value = product.Brand;
            command.Parameters.Add("@color", SqlDbType.NVarChar, ProductDbConstants.Color).Value = product.Color;
            command.Parameters.Add("@createTime", SqlDbType.DateTime).Value = product.CreateTime;
            command.Parameters.Add("@description", SqlDbType.NVarChar, ProductDbConstants.Description).Value = product.Description;
            command.Parameters.Add("@discount", SqlDbType.Float).Value = product.Discount;
            command.Parameters.Add("@expiration", SqlDbType.DateTime).Value = product.Expiration;
            command.Parameters.Add("@discountPrice", SqlDbType.Money).Value = product.DiscountPrice;
            command.Parameters.Add("@forBaby", SqlDbType.Bit).Value = product.ForBaby;
            command.Parameters.Add("@gender", SqlDbType.TinyInt).Value = product.Gender;
            command.Parameters.Add("@isDeleted", SqlDbType.Bit).Value = product.IsDeleted;
            command.Parameters.Add("@name", SqlDbType.NVarChar, ProductDbConstants.Name).Value = product.Name;
            command.Parameters.Add("@price", SqlDbType.Money).Value = product.Price;
            command.Parameters.Add("@productType", SqlDbType.NVarChar, ProductDbConstants.ProductType).Value = product.ProductType;
            command.Parameters.Add("@weight", SqlDbType.NVarChar).Value = product.Weight.AsJson();
            command.Parameters.Add("@size", SqlDbType.NVarChar, ProductDbConstants.Size).Value = product.Size;

            await using var transaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);

            try
            {
                await connection.EnsureIsOpenAsync().ConfigureAwait(false);
                await command.ExecuteNonQueryAsync().ConfigureAwait(false);

                foreach (var item in product.Images)
                {
                    await using var command2 = new SqlCommand(sql2, connection);
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = item.Id;
                    command.Parameters.Add("@productId", SqlDbType.BigInt).Value = item.ProductId;
                    command.Parameters.Add("@url", SqlDbType.VarChar).Value = item.Url;
                    command.Parameters.Add("@mainImage", SqlDbType.Bit).Value = item.MainImage;
                    await command2.ExecuteNonQueryAsync().ConfigureAwait(false);
                }

                await transaction.CommitAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw; //todo Exceptions
            }
        }

        public async Task UpdateNameAsync(string name, long id)
        {
            const string sql =
  @"UPDATE OnlineShop.Products
SET Name=@name
WHERE Id=@id;";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql);

            command.Parameters.Add("@name", SqlDbType.NVarChar, ProductDbConstants.Name).Value = name;
            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task UpdatePriceAsync(decimal price, long id)
        {
            const string sql =
                @"UPDATE OnlineShop.Products
SET Price=@price
WHERE Id=@id;";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql);

            command.Parameters.Add("@price", SqlDbType.Money).Value = price;
            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task UpdateDiscountAsync(float discount, long id)
        {
            const string sql =
                @"UPDATE OnlineShop.Products
SET Discount=@discount
WHERE Id=@id;";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql);

            command.Parameters.Add("@discount", SqlDbType.Float).Value = discount;
            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task UpdateBrandAsync(string brand, long id)
        {
            const string sql =
                @"UPDATE OnlineShop.Products
SET Brand=@brand
WHERE Id=@id;";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql);

            command.Parameters.Add("@brand", SqlDbType.NVarChar, ProductDbConstants.Brand).Value = brand;
            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task UpdateTypeAsync(string type, long id)
        {
            const string sql =
                @"UPDATE OnlineShop.Products
SET ProductType=@type
WHERE Id=@id;";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql);

            command.Parameters.Add("@type", SqlDbType.NVarChar, ProductDbConstants.Brand).Value = type;
            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task UpdateForBabyAsync(bool isBaby, long id)
        {
            const string sql =
                @"UPDATE OnlineShop.Products
SET ForBaby=@isBaby
WHERE Id=@id;";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql);

            command.Parameters.Add("@isBaby", SqlDbType.Bit).Value = isBaby;
            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task UpdateColorAsync(string color, long id)
        {
            const string sql =
                @"UPDATE OnlineShop.Products
SET Color=@color
WHERE Id=@id;";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql);

            command.Parameters.Add("@color", SqlDbType.NVarChar, ProductDbConstants.Brand).Value = color;
            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }
    }
}
