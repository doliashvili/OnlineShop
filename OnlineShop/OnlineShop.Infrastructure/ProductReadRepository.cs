using OnlineShop.Domain.AbstractRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OnlineShop.Domain.Enums;
using OnlineShop.Domain.Products.Queries;
using OnlineShop.Domain.Products.ReadModels;
using OnlineShop.Domain.Products.Records;
using OnlineShop.Infrastructure.CommonSql;

namespace OnlineShop.Infrastructure
{
    public class ProductReadRepository : IProductReadRepository
    {
        private readonly string _connectionString;

        public ProductReadRepository(DatabaseConnectionString connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public async Task<List<ProductReadModel>> GetAllProductAsync(GetAllProducts query)
        {
            const string sql =
                @"SELECT Id,Brand, Color,CreateTime,[Description],Discount,Expiration,DiscountPrice,ForBaby,Gender,IsDeleted,Name,Price,ProductType,Weight,Size,img.Url,img.MainImage,img.Id as imgId
        FROM Products
        LEFT JOIN Images img ON Id=img.ProductId;";

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

            var products = new List<ProductReadModel>(20);
            while (await reader.ReadAsync().ConfigureAwait(false)) //Todo CancellationTokens
            {
                var product = await ReadProductAsync(reader).ConfigureAwait(false);
                products.Add(product);
            }

            return products;
        }

        public Task<PagingProductModel> GetProductsAsync(GetProducts query)
        {
            const string sql =
                @"SELECT Id,Brand, Color,CreateTime,[Description],Discount,Expiration,DiscountPrice,ForBaby,Gender,IsDeleted,Name,Price,ProductType,Weight,Size,img.Url,img.MainImage,img.Id as imgId
        FROM Products
        LEFT JOIN Images img ON Id=img.ProductId;";
            throw new NotImplementedException();
        }

        public async Task<ProductReadModel> GetProductByIdAsync(GetProductById query)
        {
            const string sql =
                @"SELECT Id,Brand, Color,CreateTime,[Description],Discount,Expiration,DiscountPrice,ForBaby,Gender,IsDeleted,Name,Price,ProductType,Weight,Size,img.Url,img.MainImage,img.Id as imgId
        FROM Products
        LEFT JOIN Images img ON Id=img.ProductId;
        WHERE Id=@id";

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@id", SqlDbType.BigInt).Value = query.Id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

            if (!await reader.ReadAsync().ConfigureAwait(false))
            {
                return null;
            }

            var product = await ReadProductAsync(reader).ConfigureAwait(false);

            return product;
        }

        public Task<PagingProductModel> GetFilteredProductsAsync(GetFilteredProducts query)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetAllProductCountAsync(GetAllProductCount query)
        {
            const string sql =
                @"SELECT COUNT(Id)
        FROM Products;";

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

            return reader.AsInt32(0);
        }

        #region HelperMethods
        private string GenerateSqlFromParameters(GetFilteredProducts query)
        {
            const string sql =
                @"SELECT Id,Brand, Color,CreateTime,[Description],Discount,Expiration,DiscountPrice,ForBaby,Gender,IsDeleted,Name,Price,ProductType,Weight,Size,img.Url,img.MainImage,img.Id as imgId
        FROM Products
        LEFT JOIN Images img ON Id=img.ProductId;";

            var sb = new StringBuilder();
            sb.AppendWithAnd("Id=@id");

            if (query.Brand is not null)
                sb.AppendWithAnd("Brand=@brand");

            if (query.Color is not null)
                sb.AppendWithAnd("Color=@color");

            if (query.ForBaby is not null)
                sb.AppendWithAnd("Color=@color");

            if (query.Gender is not null)
                sb.AppendWithAnd("Color=@color");

            if (query.Name is not null)
                sb.AppendWithAnd("Color=@color");

            if (query.Size is not null)
                sb.AppendWithAnd("Color=@color");

            if (query.ProductType is not null)
                sb.AppendWithAnd("Color=@color");

            sb.AppendWithAnd("PriceFrom >= @priceFrom");
            sb.AppendWithAnd("PriceTo <= @priceTo");

            return sb.GetSqlFrom(sql);
        }

        private static async Task<ProductReadModel> ReadProductAsync(SqlDataReader reader)
        {
            var idx = 0;
            var id = reader.AsInt64(idx++);
            var price = reader.AsDecimal(idx++);
            var isDeleted = reader.AsBooleanOrNull(idx++);
            var color = reader.AsStringOrNull(idx++);
            var brand = reader.AsStringOrNull(idx++);
            var productType = reader.AsString(idx++);
            var weight = reader.AsJsonOrNull<Weight>(idx++);
            var name = reader.AsStringOrNull(idx++);
            var description = reader.AsStringOrNull(idx++);
            var gender = reader.AsEnumOrNull<Gender>(idx++);
            var isBaby = reader.AsBooleanOrNull(idx++);
            var size = reader.AsStringOrNull(idx++);
            var discount = reader.AsFloatOrNull(idx++);
            var createTime = reader.AsDateTimeOrNull(idx++);
            var expiration = reader.AsDateTimeOrNull(idx++);
            var images = await ReadProductImageListAsync(reader).ConfigureAwait(false);

            var product = new ProductReadModel()
            {
                Id = id,
                Price = price,
                IsDeleted = isDeleted,
                Color = color,
                Brand = brand,
                ProductType = productType,
                Weight = weight,
                Name = name,
                Description = description,
                Gender = gender,
                ForBaby = isBaby,
                Size = size,
                Discount = discount,
                CreateTime = createTime,
                Expiration = expiration,
                Images = images,
            };

            return product;
        }

        private static async Task<List<ProductReadModelImage>> ReadProductImageListAsync(SqlDataReader reader)
        {
            var productImages = new List<ProductReadModelImage>(6);
            while (await reader.ReadAsync().ConfigureAwait(false))
            {
                var image = ReadProductImage(reader);
                productImages.Add(image);
            }

            return productImages;
        }

        private static ProductReadModelImage ReadProductImage(SqlDataReader reader)
        {
            var idx = 0;
            var id = reader.AsInt64(idx++);
            var url = reader.AsStringOrNull(idx++);
            var isMainImage = reader.AsBoolean(idx);

            var productImage = new ProductReadModelImage()
            {
                Id = id,
                Url = url,
                MainImage = isMainImage,
            };

            return productImage;
        }
        #endregion HelperMethods
    }
}
