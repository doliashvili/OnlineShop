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
        public long UserId { get; set; }

        public GetAllCarts(long userId)
        {
            UserId = userId;
        }
    }
}