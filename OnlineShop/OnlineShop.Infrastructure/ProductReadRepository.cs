﻿using OnlineShop.Domain.AbstractRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Babel;
using Microsoft.Data.SqlClient;
using OnlineShop.Domain.Enums;
using OnlineShop.Domain.Products.Queries;
using OnlineShop.Domain.Products.ReadModels;
using OnlineShop.Domain.Products.Records;
using OnlineShop.Infrastructure.CommonSql;
using OnlineShop.Infrastructure.Constants;

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

        public async Task<PagingProductModel> GetProductsAsync(GetProducts query)
        {
            var skip = (query.Page - 1) * query.PageSize;
            string sql =
                $@"SELECT Id,Brand,Color,CreateTime,[Description],Discount,Expiration,DiscountPrice,ForBaby,Gender,IsDeleted,Name,Price,ProductType,Weight,Size,img.Url,img.MainImage,img.Id as imgId,COUNT(Id)OVER() AS TotalCount
        FROM Products
        LEFT JOIN Images img ON Id=img.ProductId
        ORDER BY Id OFFSET {skip} ROWS FETCH NEXT {query.PageSize} ROWS ONLY;";

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

            var totalCount = (int)reader["TotalCount"];

            var pageCount = CountPages(query.PageSize, totalCount);

            return new PagingProductModel(products, pageCount, query.Page);
        }

        public async Task<ProductReadModel> GetProductByIdAsync(GetProductById query)
        {
            const string sql =
                @"SELECT Id,Brand, Color,CreateTime,[Description],Discount,Expiration,DiscountPrice,ForBaby,Gender,IsDeleted,Name,Price,ProductType,Weight,Size,img.Url,img.MainImage,img.Id as imgId
        FROM Products
        LEFT JOIN Images img ON Id=img.ProductId
        WHERE Id=@id;";

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

        public async Task<PagingProductModel> GetFilteredProductsAsync(GetFilteredProducts query)
        {
            string sql = GenerateSqlFromParameters(query);

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@brand", SqlDbType.NVarChar, ProductDbConstants.Brand).SetValue(query.Brand);
            command.Parameters.Add("@color", SqlDbType.NVarChar, ProductDbConstants.Color).SetValue(query.Color);
            command.Parameters.Add("@forBaby", SqlDbType.Bit).SetValue(query.ForBaby);
            command.Parameters.Add("@gender", SqlDbType.TinyInt).SetValue(query.Gender);
            command.Parameters.Add("@name", SqlDbType.NVarChar, ProductDbConstants.Name).SetValue(query.Name);
            command.Parameters.Add("@priceFrom", SqlDbType.Money).SetValue(query.PriceFrom);
            command.Parameters.Add("@priceTo", SqlDbType.Money).SetValue(query.PriceTo);
            command.Parameters.Add("@productType", SqlDbType.NVarChar, ProductDbConstants.ProductType).SetValue(query.ProductType);
            command.Parameters.Add("@size", SqlDbType.NVarChar, ProductDbConstants.Size).SetValue(query.Size);

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

            var products = new List<ProductReadModel>(20);
            while (await reader.ReadAsync().ConfigureAwait(false)) //Todo CancellationTokens
            {
                var product = await ReadProductAsync(reader).ConfigureAwait(false);
                products.Add(product);
            }

            var totalCount = (int)reader["TotalCount"];

            var pageCount = CountPages(query.PageSize, totalCount);

            return new PagingProductModel(products, pageCount, query.Page);
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

        private int CountPages(int pageSize, int totalCount)
        {
            var total = totalCount / pageSize;

            if (totalCount % pageSize > 0)
                total++;

            return total;
        }

        private string GenerateSqlFromParameters(GetFilteredProducts query)
        {
            const string sql =
                @"SELECT Id,Brand, Color,CreateTime,[Description],Discount,Expiration,DiscountPrice,ForBaby,Gender,IsDeleted,Name,Price,ProductType,Weight,Size,img.Url,img.MainImage,img.Id as imgId,COUNT(Id)OVER() AS TotalCount
        FROM Products
        LEFT JOIN Images img ON Id=img.ProductId";

            var sb = new StringBuilder();

            if (query.Brand is not null)
                sb.AppendWithAnd("Brand=@brand");

            if (query.Color is not null)
                sb.AppendWithAnd("Color=@color");

            if (query.ForBaby is not null)
                sb.AppendWithAnd("ForBaby=@forBaby");

            if (query.Gender is not null)
                sb.AppendWithAnd("Gender=@gender");

            if (query.Name is not null)
                sb.AppendWithAnd("Name=@name");

            if (query.Size is not null)
                sb.AppendWithAnd("Size=@size");

            if (query.ProductType is not null)
                sb.AppendWithAnd("ProductType=@productType");

            sb.AppendWithAnd("Price >= @priceFrom || DiscountPrice >= @priceFrom");
            sb.AppendWithAnd("Price <= @priceTo || DiscountPrice <= @priceTo");
            sb.AppendWhereIfHaveCondition(sql);

            var skip = (query.Page - 1) * query.PageSize;

            sb.Append("ORDER BY Id OFFSET ").Append(skip).Append(" ROWS FETCH NEXT ").Append(query.PageSize).AppendLine(" ROWS ONLY");

            return sb.ToString();
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
            var expiration = reader.AsDateTimeOrNull(idx);
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
