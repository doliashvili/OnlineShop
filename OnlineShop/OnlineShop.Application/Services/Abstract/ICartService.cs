﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Domain.CommonModels.Carts;

namespace OnlineShop.Application.Services.Abstract
{
    public interface ICartService
    {
        Task<bool> AddCartAsync(CartGuest cart, TimeSpan expire);

        Task<bool> RemoveAsync(string key);

        Task<CartGuest> GetCartAsync(string key);
    }
}