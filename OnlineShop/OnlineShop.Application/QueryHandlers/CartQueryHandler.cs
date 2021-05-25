using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cqrs;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.Carts.Queries;
using OnlineShop.Domain.Carts.ReadModels;

namespace OnlineShop.Application.QueryHandlers
{
    public class CartQueryHandler : IQueryHandler<GetAllCarts, List<CartReadModel>>,
        IQueryHandler<GetCartsCount, int>
    {
        private readonly ICartReadRepository _cartReadRepository;

        public CartQueryHandler(ICartReadRepository cartReadRepository)
        {
            _cartReadRepository = cartReadRepository;
        }

        public Task<List<CartReadModel>> HandleAsync(GetAllCarts query)
        {
            return _cartReadRepository.GetCartsAsync(query.UserId);
        }

        public Task<int> HandleAsync(GetCartsCount query)
        {
            return _cartReadRepository.GetCartsCountAsync(query.UserId);
        }
    }
}