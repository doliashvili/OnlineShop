using Cqrs;
using OnlineShop.Domain.Products.Commands;
using System;
using System.Threading.Tasks;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.Products.DomainObjects;

namespace OnlineShop.Application.CommandHandlers
{
    public sealed class ProductCommandHandler : ICommandHandler<CreateProductCommand>,
        ICommandHandler<AddProductImageCommand>,
        ICommandHandler<DeleteProductImageCommand>,
        ICommandHandler<DeleteProductCommand>,
        ICommandHandler<ChangeProductNameCommand>,
        ICommandHandler<ChangeProductPriceCommand>,
        ICommandHandler<ChangeProductBrandCommand>,
        ICommandHandler<ChangeProductColorCommand>,
        ICommandHandler<ChangeProductTypeCommand>,
        ICommandHandler<ChangeProductDiscountCommand>,
        ICommandHandler<ChangeProductForBabyCommand>
    {
        private readonly IProductWriteRepository _writeRepository;

        public ProductCommandHandler(IProductWriteRepository writeRepository)
        {
            _writeRepository = writeRepository;
        }
        public async Task HandleAsync(CreateProductCommand command)
        {
            var product = new Product(command);

            await _writeRepository.CreateAsync(product).ConfigureAwait(false);
        }

        public Task HandleAsync(AddProductImageCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleAsync(DeleteProductImageCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleAsync(DeleteProductCommand command)
        {
            throw new NotImplementedException();
        }

        public async Task HandleAsync(ChangeProductNameCommand command)
        {
            await _writeRepository.UpdateNameAsync(command.Name, command.Id).ConfigureAwait(false);
        }

        public async Task HandleAsync(ChangeProductPriceCommand command)
        {
            await _writeRepository.UpdatePriceAsync(command.Price, command.Id).ConfigureAwait(false);
        }

        public async Task HandleAsync(ChangeProductBrandCommand command)
        {
            await _writeRepository.UpdateBrandAsync(command.Brand, command.Id).ConfigureAwait(false);
        }

        public async Task HandleAsync(ChangeProductColorCommand command)
        {
            await _writeRepository.UpdateColorAsync(command.Color, command.Id).ConfigureAwait(false);
        }

        public async Task HandleAsync(ChangeProductTypeCommand command)
        {
            await _writeRepository.UpdateTypeAsync(command.ProductType, command.Id).ConfigureAwait(false);
        }

        public async Task HandleAsync(ChangeProductDiscountCommand command)
        {
           await _writeRepository.UpdateDiscountAsync(command.Discount, command.Id).ConfigureAwait(false);
        }

        public async Task HandleAsync(ChangeProductForBabyCommand command)
        {
           await _writeRepository.UpdateForBabyAsync(command.ForBaby, command.Id).ConfigureAwait(false);
        }
    }
}
