using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineShop.Domain.Products.Queries;
using OnlineShop.Domain.Products.ReadModels;

namespace OnlineShop.Domain.AbstractRepository
{
    public interface IProductReadRepository
    {
        public Task<List<ProductReadModel>> GetAllProductAsync(GetAllProducts query);
        public Task<PagingProductModel> GetProductsAsync(GetProducts query);
        public Task<ProductReadModel?> GetProductByIdAsync(GetProductById query);
        public Task<PagingProductModel> GetFilteredProductsAsync(GetFilteredProducts query);
        public Task<int> GetAllProductCountAsync(GetAllProductCount query);
    }
}
