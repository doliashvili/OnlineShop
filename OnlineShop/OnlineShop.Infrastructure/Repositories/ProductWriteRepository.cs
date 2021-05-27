using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using IdGeneration.GeneratorWrapper;
using Microsoft.Data.SqlClient;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.Categories.Commands;
using OnlineShop.Domain.Categories.DomainObjects;
using OnlineShop.Domain.Extensions;
using OnlineShop.Domain.Products.DomainObjects;
using OnlineShop.Infrastructure.CommonSql;
using OnlineShop.Infrastructure.Constants;

namespace OnlineShop.Infrastructure.Repositories
{
    public sealed class ProductWriteRepository : IProductWriteRepository
    {
        private readonly ICategoryWriteRepository _categoryRepository;
        private readonly string _connectionString;

        public ProductWriteRepository(DatabaseConnectionString connectionString, ICategoryWriteRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _connectionString = connectionString.Value;
        }

        public async Task CreateAsync(Product product)//Todo CancelationTokens
        {
            const string sql =
  @"INSERT INTO Products (Id,Brand,Color,CreateTime,[Description],Discount,Expiration,DiscountPrice,ForBaby,Gender,IsDeleted,Name,Price,Quantity,ProductType,Weight,Size)
VALUES (@id,@brand,@color,@createTime,@description,@discount,@expiration,@discountPrice,@forBaby,@gender,@isDeleted,@name,@price,@quantity,@productType,@weight,@size);";

            const string sql2 =
  @"INSERT INTO Images (Id,MainImage,Url,ProductId)
VALUES (@imgId,@mainImage,@url,@productId);";

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);
            command.Parameters.Add("@id", SqlDbType.BigInt).Value = product.Id;
            command.Parameters.Add("@brand", SqlDbType.NVarChar, ProductDbConstants.Brand).SetValue(product.Brand);
            command.Parameters.Add("@color", SqlDbType.NVarChar, ProductDbConstants.Color).SetValue(product.Color);
            command.Parameters.Add("@createTime", SqlDbType.DateTime).SetValue(product.CreateTime);
            command.Parameters.Add("@description", SqlDbType.NVarChar, ProductDbConstants.Description).SetValue(product.Description);
            command.Parameters.Add("@discount", SqlDbType.Float).SetValue(product.Discount);
            command.Parameters.Add("@expiration", SqlDbType.DateTime).SetValue(product.Expiration);
            command.Parameters.Add("@discountPrice", SqlDbType.SmallMoney).SetValue(product.DiscountPrice);
            command.Parameters.Add("@forBaby", SqlDbType.Bit).SetValue(product.ForBaby);
            command.Parameters.Add("@gender", SqlDbType.TinyInt).SetValue(product.Gender);
            command.Parameters.Add("@isDeleted", SqlDbType.Bit).SetValue(product.IsDeleted);
            command.Parameters.Add("@name", SqlDbType.NVarChar, ProductDbConstants.Name).SetValue(product.Name);
            command.Parameters.Add("@price", SqlDbType.SmallMoney).Value = product.Price;
            command.Parameters.Add("@quantity", SqlDbType.TinyInt).Value = product.Quantity;
            command.Parameters.Add("@productType", SqlDbType.NVarChar, ProductDbConstants.ProductType).Value = product.ProductType;
            command.Parameters.Add("@weight", SqlDbType.NVarChar).SetValue(product.Weight.AsJson());
            command.Parameters.Add("@size", SqlDbType.NVarChar, ProductDbConstants.Size).SetValue(product.Size);

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
            command.Transaction = transaction;
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            try
            {
                foreach (var item in product.Images)
                {
                    await using var command2 = new SqlCommand(sql2, connection);
                    command2.Transaction = transaction;
                    command2.Parameters.Add("@imgId", SqlDbType.BigInt).Value = IdGenerator.NewId;
                    command2.Parameters.Add("@mainImage", SqlDbType.Bit).Value = item.MainImage;
                    command2.Parameters.Add("@url", SqlDbType.VarChar, ProductDbConstants.Url).Value = item.Url;
                    command2.Parameters.Add("@productId", SqlDbType.BigInt).Value = product.Id;
                    await command2.ExecuteNonQueryAsync().ConfigureAwait(false);
                }

                await transaction.CommitAsync().ConfigureAwait(false);
                await _categoryRepository.AddCategoryAsync(new Category(new CreateCategoryCommand(null!, product.ProductType))).ConfigureAwait(false);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw; //todo Exceptions
            }
        }

        public async Task UpdateNameAsync(string name, long id)
        {
            const string sql =
  @"UPDATE Products
SET Name=@name
WHERE Id=@id;";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

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
            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@price", SqlDbType.SmallMoney).Value = price;
            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task UpdateDiscountAsync(float discount, long id)
        {
            const string sql =
  @"UPDATE Products
SET Discount=@discount
WHERE Id=@id;";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@discount", SqlDbType.Float).Value = discount;
            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task UpdateBrandAsync(string brand, long id)
        {
            const string sql =
  @"UPDATE Products
SET Brand=@brand
WHERE Id=@id;";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@brand", SqlDbType.NVarChar, ProductDbConstants.Brand).Value = brand;
            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task UpdateTypeAsync(string type, long id)
        {
            const string sql =
  @"UPDATE Products
SET ProductType=@type
WHERE Id=@id;";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@type", SqlDbType.NVarChar, ProductDbConstants.Brand).Value = type;
            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task UpdateForBabyAsync(bool isBaby, long id)
        {
            const string sql =
  @"UPDATE Products
SET ForBaby=@isBaby
WHERE Id=@id;";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@isBaby", SqlDbType.Bit).Value = isBaby;
            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task UpdateColorAsync(string color, long id)
        {
            const string sql =
 @"UPDATE Products
SET Color=@color
WHERE Id=@id;";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@color", SqlDbType.NVarChar, ProductDbConstants.Brand).Value = color;
            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task AddImagesAsync(List<ProductImage> images)
        {
            const string sql = @"
  INSERT INTO [dbo].[Images]
([Id],[Url],[MainImage],[ProductId])
VALUES(@id,@url,@mainImage,@productId)";

            await using var connection = new SqlConnection(_connectionString);

            for (var i = 0; i < images.Count; i++)
            {
                await using var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@id", SqlDbType.BigInt).Value = IdGenerator.NewId;
                command.Parameters.Add("@mainImage", SqlDbType.Bit).Value = images[i].MainImage;
                command.Parameters.Add("@url", SqlDbType.VarChar, ProductDbConstants.Url).Value = images[i].Url;
                command.Parameters.Add("@productId", SqlDbType.BigInt).Value = images[i].ProductId;
                await connection.EnsureIsOpenAsync().ConfigureAwait(false);
                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async Task DeleteImageAsync(long imageId)
        {
            const string sql = "DELETE FROM [dbo].[Images] WHERE Id=@id";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@id", SqlDbType.BigInt).Value = imageId;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task DeleteProductAsync(long id)
        {
            const string sql =
 @"UPDATE Products
SET IsDeleted=1
WHERE Id=@id;";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task UpdateQuantityAsync(byte quantity, long id)
        {
            const string sql =
 @"UPDATE Products
SET Quantity=@quantity
WHERE Id=@id;";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@quantity", SqlDbType.TinyInt).Value = quantity;
            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }
    }
}