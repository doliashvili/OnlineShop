using System.Threading.Tasks;
using Cqrs;
using Cqrs.ApiGenerator;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.Carts.Commands;
using OnlineShop.Domain.Carts.Entity;

namespace OnlineShop.Application.CommandHandlers
{
    [ApiGen]
    public class CartCommandHandler : ICommandHandler<AddCartCommand>,
        ICommandHandler<DeleteCartCommand>,
        ICommandHandler<UpdateCartQuantityCommand>
    {
        private readonly ICartWriteRepository _cartWriteRepository;

        public CartCommandHandler(ICartWriteRepository cartWriteRepository)
        {
            _cartWriteRepository = cartWriteRepository;
        }

        public Task HandleAsync(AddCartCommand command)
        {
            var cart = new Cart(command);
            return _cartWriteRepository.AddCartAsync(cart);
        }

        public Task HandleAsync(DeleteCartCommand command)
        {
            return _cartWriteRepository.RemoveCartAsync(command.Id);
        }

        public Task HandleAsync(UpdateCartQuantityCommand command)
        {
            return _cartWriteRepository.UpdateCartQuantityAsync(command.Id, command.ProductId, command.Quantity);
        }
    }
}