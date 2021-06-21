using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.Enums;
using OnlineShop.Domain.Products.Queries;
using OnlineShop.Domain.Products.ReadModels;
using OnlineShop.Domain.Products.Records;
using OnlineShop.Infrastructure.CommonSql;
using OnlineShop.Infrastructure.Constants;

namespace OnlineShop.Infrastructure.Repositories
{
    public sealed class ProductReadRepository : IProductReadRepository
    {
        private readonly string _connectionString;

        public ProductReadRepository(DatabaseConnectionString connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public async Task<List<ProductReadModel>> GetAllProductAsync(GetAllProducts query)
        {
            const string sql =
                @"SELECT dbo.Products.Id,Brand,Color,CreateTime,[Description],Discount,Expiration,DiscountPrice,ForBaby,Gender,IsDeleted,[Name],Price,Quantity,ProductType,[Weight],Size,Images.[Url],Images.MainImage,Images.Id AS ImgId,Images.ProductId
        FROM Products
        LEFT JOIN Images ON dbo.Products.Id=Images.ProductId;";

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

            var products = new List<ProductReadModel>(20);
            return await ProductListReaderAsync(reader, products).ConfigureAwait(false);
        }

        public async Task<PagingProductModel> GetProductsAsync(GetProducts query)
        {
            var skip = (query.Page - 1) * query.PageSize;
            string sql =
 $@"WITH pg AS
  (SELECT Products.Id, c = COUNT(*) OVER()
      FROM Products
      ORDER BY Products.Id
      OFFSET {query.PageSize} * {skip} ROWS
      FETCH NEXT {query.PageSize} ROWS ONLY)
SELECT dbo.Products.Id,Brand,Color,CreateTime,[Description],Discount,Expiration,DiscountPrice,ForBaby,Gender,IsDeleted,[Name],Price,Quantity,ProductType,[Weight],Size,Images.[Url],Images.MainImage,Images.Id AS ImgId,Images.ProductId
        FROM Products
        LEFT JOIN Images ON dbo.Products.Id=Images.ProductId
		INNER JOIN pg ON pg.Id= dbo.Products.Id
        WHERE IsDeleted=0;";

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

            var products = new List<ProductReadModel>(20);
            products = await ProductListReaderAsync(reader, products).ConfigureAwait(false);

            var totalCount = await GetAllProductCountAsync(new GetAllProductCount()).ConfigureAwait(false);

            var pageCount = CountPages(query.PageSize, totalCount);

            return new PagingProductModel(products, pageCount, query.Page);
        }

        public async Task<ProductReadModel?> GetProductByIdAsync(GetProductById query)
        {
            const string sql =
                @"SELECT dbo.Products.Id,Brand,Color,CreateTime,[Description],Discount,Expiration,DiscountPrice,ForBaby,Gender,IsDeleted,[Name],Price,Quantity,ProductType,[Weight],Size,Images.[Url],Images.MainImage,Images.Id AS ImgId,Images.ProductId
        FROM Products
        LEFT JOIN Images ON dbo.Products.Id=Images.ProductId
        WHERE dbo.Products.Id=@id AND IsDeleted=0;";

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@id", SqlDbType.BigInt).Value = query.Id;

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

            var product = await ReadProductAsync(reader).ConfigureAwait(false);

            return product;
        }

        public async Task<PagingProductModel> GetFilteredProductsAsync(GetFilteredProducts query)
        {
            string sql = GenerateSqlFromFilteredParameters(query);

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@brand", SqlDbType.NVarChar, ProductDbConstants.Brand).SetValue(query.Brand);
            command.Parameters.Add("@color", SqlDbType.NVarChar, ProductDbConstants.Color).SetValue(query.Color);
            command.Parameters.Add("@forBaby", SqlDbType.Bit).SetValue(query.ForBaby);
            command.Parameters.Add("@gender", SqlDbType.TinyInt).SetValue(query.Gender);
            command.Parameters.Add("@name", SqlDbType.NVarChar, ProductDbConstants.Name).SetValue(query.Name);
            command.Parameters.Add("@priceFrom", SqlDbType.SmallMoney).SetValue(query.PriceFrom);
            command.Parameters.Add("@priceTo", SqlDbType.SmallMoney).SetValue(query.PriceTo);
            command.Parameters.Add("@productType", SqlDbType.NVarChar, ProductDbConstants.ProductType).SetValue(query.ProductType);
            command.Parameters.Add("@size", SqlDbType.NVarChar, ProductDbConstants.Size).SetValue(query.Size);

            await connection.EnsureIsOpenAsync().ConfigureAwait(false);
            await using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

            var products = new List<ProductReadModel>(20);
            products = await ProductListReaderAsync(reader, products).ConfigureAwait(false);

            var totalCount = 2;//todo

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

            if (!await reader.ReadAsync().ConfigureAwait(false))
            {
                return 0;
            }

            var count = reader.AsInt32(0);
            return count;
        }

        #region HelperMethods

        private static int CountPages(int pageSize, int totalCount)
        {
            var total = totalCount / pageSize;

            if (totalCount % pageSize > 0)
                total++;

            return total;
        }

        private static string GenerateSqlFromFilteredParameters(GetFilteredProducts query)
        {
            var skip = (query.Page - 1) * query.PageSize;
            string sql =
                $@"WITH pg AS
  (SELECT Products.Id, c = COUNT(*) OVER()
      FROM Products
      ORDER BY Products.Id
      OFFSET {query.PageSize} * {skip} ROWS
      FETCH NEXT {query.PageSize} ROWS ONLY)
SELECT dbo.Products.Id,Brand,Color,CreateTime,[Description],Discount,Expiration,DiscountPrice,ForBaby,Gender,IsDeleted,[Name],Price,Quantity,ProductType,[Weight],Size,Images.[Url],Images.MainImage,Images.Id AS ImgId,Images.ProductId
        FROM Products
        LEFT JOIN Images ON dbo.Products.Id=Images.ProductId
		INNER JOIN pg ON pg.Id= dbo.Products.Id";

            var sb = new StringBuilder();

            sb.AppendLine(sql);
            sb.AppendLine("WHERE IsDeleted=0");

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

            sb.AppendWithAnd("(Price >= @priceFrom OR DiscountPrice >= @priceFrom)");
            sb.AppendWithAnd("(Price <= @priceTo OR DiscountPrice <= @priceTo)");
            // sb.AppendWhereIfHaveCondition(sql);

            return sb.ToString();
        }

        private static async Task<List<ProductReadModel>> ProductListReaderAsync(SqlDataReader reader, List<ProductReadModel> products)
        {
            while (await reader.ReadAsync().ConfigureAwait(false)) //Todo CancellationTokens
            {
                var idx = 0;
                var id = reader.AsInt64(idx++);
                var brand = reader.AsStringOrNull(idx++);
                var color = reader.AsStringOrNull(idx++);
                var createTime = reader.AsDateTimeOrNull(idx++);
                var description = reader.AsStringOrNull(idx++);
                var discount = reader.AsFloatOrNull(idx++);
                var expiration = reader.AsDateTimeOrNull(idx++);
                var discountPrice = reader.AsDecimalOrNull(idx++);
                var isBaby = reader.AsBooleanOrNull(idx++);
                var gender = reader.AsEnumOrNull<Gender>(idx++);
                var isDeleted = reader.AsBooleanOrNull(idx++);
                var name = reader.AsStringOrNull(idx++);
                var price = reader.AsDecimal(idx++);
                var quantity = reader.AsInt16(idx++);
                var productType = reader.AsString(idx++);
                var weight = reader.AsJsonOrNull<Weight>(idx++);
                var size = reader.AsStringOrNull(idx++);
                var url = reader.AsString(idx++);
                var isMainImage = reader.AsBoolean(idx++);
                var imgId = reader.AsInt64(idx++);
                var productId = reader.AsInt64(idx);

                var product = products.Find(x => x.Id == productId);
                if (product == null)
                {
                    product = new ProductReadModel()
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
                        Quantity = quantity,
                        CreateTime = createTime,
                        DiscountPrice = discountPrice,
                        Expiration = expiration,
                        Images = new List<ProductReadModelImage>(6)
                         {
                          new () {
                             Id = imgId,
                             Url = url,
                             MainImage = isMainImage,
                             ProductId = productId,
                          }
                         }
                    };
                    products.Add(product);
                }
                else
                {
                    product.Images.Add(new()
                    {
                        Id = imgId,
                        Url = url,
                        MainImage = isMainImage,
                        ProductId = productId
                    });
                }
            }

            return products;
        }

        private static async Task<ProductReadModel?> ReadProductAsync(SqlDataReader reader)
        {
            ProductReadModel? product = null;
            while (await reader.ReadAsync().ConfigureAwait(false)) //Todo CancellationTokens
            {
                var idx = 0;
                var id = reader.AsInt64(idx++);
                var brand = reader.AsStringOrNull(idx++);
                var color = reader.AsStringOrNull(idx++);
                var createTime = reader.AsDateTimeOrNull(idx++);
                var description = reader.AsStringOrNull(idx++);
                var discount = reader.AsFloatOrNull(idx++);
                var expiration = reader.AsDateTimeOrNull(idx++);
                var discountPrice = reader.AsDecimalOrNull(idx++);
                var isBaby = reader.AsBooleanOrNull(idx++);
                var gender = reader.AsEnumOrNull<Gender>(idx++);
                var isDeleted = reader.AsBooleanOrNull(idx++);
                var name = reader.AsStringOrNull(idx++);
                var price = reader.AsDecimal(idx++);
                var quantity = reader.AsByte(idx++);
                var productType = reader.AsString(idx++);
                var weight = reader.AsJsonOrNull<Weight>(idx++);
                var size = reader.AsStringOrNull(idx++);
                var url = reader.AsString(idx++);
                var isMainImage = reader.AsBoolean(idx++);
                var imgId = reader.AsInt64(idx++);
                var productId = reader.AsInt64(idx);

                if (product is null)
                {
                    product = new ProductReadModel()
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
                        Quantity = quantity,
                        CreateTime = createTime,
                        DiscountPrice = discountPrice,
                        Expiration = expiration,
                        Images = new List<ProductReadModelImage>(6)
                         {
                          new () {
                             Id = imgId,
                             Url = url,
                             MainImage = isMainImage,
                             ProductId = productId,
                          }
                         }
                    };
                }
                else
                {
                    product.Images.Add(new()
                    {
                        Id = imgId,
                        Url = url,
                        MainImage = isMainImage,
                        ProductId = productId
                    });
                }
            }

            return product;
        }

        #endregion HelperMethods
    }
}