﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdGeneration.GeneratorWrapper;
using OnlineShop.Domain.Carts.Commands;

namespace OnlineShop.Domain.Carts.DomainObjects
{
    public sealed partial class Cart
    {
        public Cart(long id) : base(id)
        {
        }

        public Cart(AddCartCommand command) : this(IdGenerator.NewId)
        {
            ProductId = command.ProductId;
            Quantity = command.Quantity;
            UserId = command.UserId;
        }
    }
}