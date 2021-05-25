using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreModels.Messaging;

namespace OnlineShop.Domain.Carts.Queries
{
    public class GetCartsCount : IQuery<int>
    {
        public string UserId { get; set; }

        public GetCartsCount(string userId)
        {
            UserId = userId;
        }

        public GetCartsCount()
        {
        }
    }
}