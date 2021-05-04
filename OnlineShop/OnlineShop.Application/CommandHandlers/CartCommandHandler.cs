using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cqrs;
using Cqrs.ApiGenerator;
using OnlineShop.Domain.Carts.Commands;

namespace OnlineShop.Application.CommandHandlers
{
    [ApiGen]
    public class CartCommandHandler : ICommandHandler<AddCartCommand>,
        ICommandHandler<DeleteCartCommand>,
        ICommandHandler<UpdateCartQuantityCommand>
    {
        public Task HandleAsync(AddCartCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleAsync(DeleteCartCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleAsync(UpdateCartQuantityCommand command)
        {
            throw new NotImplementedException();
        }
    }
}