using System.Collections.Generic;
using System.Threading.Tasks;
using Cqrs;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.Products.Queries;
using OnlineShop.Domain.Products.ReadModels;

namespace OnlineShop.Application.QueryHandlers
{
    public sealed class ProductQueryHandler : IQueryHandler<GetAllProducts, List<ProductReadModel>>,
        IQueryHandler<GetAllProductCount,int>
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
    }
}
