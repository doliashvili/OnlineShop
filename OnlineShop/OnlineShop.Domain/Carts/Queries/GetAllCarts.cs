using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreModels.Messaging;
using OnlineShop.Domain.Carts.ReadModels;

namespace OnlineShop.Domain.Carts.Queries
{
    public class GetAllCarts : IQuery<List<CartReadModel>>
    {
        public List<long> CartIds { get; set; }
        public long UserId { get; set; }

        public GetAllCarts(List<long> cartIds, long userId)
        {
            CartIds = cartIds;
            UserId = userId;
        }
    }
}