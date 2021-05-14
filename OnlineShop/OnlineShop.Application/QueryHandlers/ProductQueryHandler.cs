using System.Collections.Generic;
using System.Threading.Tasks;
using Cqrs;
using OnlineShop.Application.Constants;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.Products.Queries;
using OnlineShop.Domain.Products.ReadModels;

namespace OnlineShop.Application.QueryHandlers
{
    public sealed class ProductQueryHandler : IQueryHandler<GetAllProducts, List<ProductReadModel>>,
        IQueryHandler<GetAllProductCount, int>,
        IQueryHandler<GetProducts, PagingProductModel>,
        IQueryHandler<GetFilteredProducts, PagingProductModel>,
        IQueryHandler<GetProductById, ProductReadModel>
    {
        private readonly IProductReadRepository _readRepository;

        public ProductQueryHandler(IProductReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public Task<List<ProductReadModel>> HandleAsync(GetAllProducts query)
        {
            return _readRepository.GetAllProductAsync(query);
        }

        public Task<int> HandleAsync(GetAllProductCount query)
        {
            return _readRepository.GetAllProductCountAsync(query);
        }

        public Task<PagingProductModel> HandleAsync(GetProducts query)
        {
            if (query.Page <= 0)
            {
                query.Page = 1;
            }

            if (query.PageSize <= 0)
            {
                query.PageSize = 10;
            }

            return _readRepository.GetProductsAsync(query);
        }

        public Task<PagingProductModel> HandleAsync(GetFilteredProducts query)
        {
            if (query.PriceFrom is null || query.PriceFrom < ProductConstants.MinPrice)
            {
                query.PriceFrom = ProductConstants.MinPrice;
            }

            if (query.PriceTo is null || query.PriceFrom > ProductConstants.MaxPrice)
            {
                query.PriceTo = ProductConstants.MaxPrice;
            }

            if (query.Page <= 0)
            {
                query.Page = 1;
            }

            if (query.PageSize <= 0)
            {
                query.PageSize = 10;
            }

            return _readRepository.GetFilteredProductsAsync(query);
        }

        public Task<ProductReadModel> HandleAsync(GetProductById query)
        {
            return _readRepository.GetProductByIdAsync(query);
        }
    }
}