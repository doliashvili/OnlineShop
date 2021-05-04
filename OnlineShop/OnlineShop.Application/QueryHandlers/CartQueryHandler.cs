﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cqrs;
using OnlineShop.Domain.Carts.Queries;
using OnlineShop.Domain.Carts.ReadModels;

namespace OnlineShop.Application.QueryHandlers
{
    public class CartQueryHandler : IQueryHandler<GetAllCarts, List<CartReadModel>>
    {
        public Task<List<CartReadModel>> HandleAsync(GetAllCarts query)
        {
            throw new NotImplementedException();
        }
    }
}