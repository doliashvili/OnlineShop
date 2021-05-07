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
        public string UserId { get; set; }

        public GetAllCarts(string userId)
        {
            UserId = userId;
        }

        public GetAllCarts()
        {
        }
    }
}