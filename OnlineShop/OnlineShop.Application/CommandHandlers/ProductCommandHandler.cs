using Cqrs;
using OnlineShop.Domain.Products.Commands;
using System;
using System.Threading.Tasks;
using Cqrs.ApiGenerator;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.Products.DomainObjects;

namespace OnlineShop.Application.CommandHandlers
{
    [ApiGen]
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
        ICommandHandler<ChangeProductForBabyCommand>,
        ICommandHandler<ChangeProductQuantityCommand>
    {
        private readonly IProductWriteRepository _writeRepository;

        public ProductCommandHandler(IProductWriteRepository writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public Task HandleAsync(CreateProductCommand command)
        {
            var product = new Product(command);

            return _writeRepository.CreateAsync(product);
        }

        public Task HandleAsync(AddProductImageCommand command)
        {
            return _writeRepository.AddImagesAsync(command.ProductImages);
        }

        public Task HandleAsync(DeleteProductImageCommand command)
        {
            return _writeRepository.DeleteImageAsync(command.ImageId);
        }

        public Task HandleAsync(DeleteProductCommand command)
        {
            return _writeRepository.DeleteProductAsync(command.Id);
        }

        public Task HandleAsync(ChangeProductNameCommand command)
        {
            return _writeRepository.UpdateNameAsync(command.Name, command.Id);
        }

        public Task HandleAsync(ChangeProductPriceCommand command)
        {
            return _writeRepository.UpdatePriceAsync(command.Price, command.Id);
        }

        public Task HandleAsync(ChangeProductBrandCommand command)
        {
            return _writeRepository.UpdateBrandAsync(command.Brand, command.Id);
        }

        public Task HandleAsync(ChangeProductColorCommand command)
        {
            return _writeRepository.UpdateColorAsync(command.Color, command.Id);
        }

        public Task HandleAsync(ChangeProductTypeCommand command)
        {
            return _writeRepository.UpdateTypeAsync(command.ProductType, command.Id);
        }

        public Task HandleAsync(ChangeProductDiscountCommand command)
        {
            return _writeRepository.UpdateDiscountAsync(command.Discount, command.Id);
        }

        public Task HandleAsync(ChangeProductForBabyCommand command)
        {
            return _writeRepository.UpdateForBabyAsync(command.ForBaby, command.Id);
        }

        public Task HandleAsync(ChangeProductQuantityCommand command)
        {
            return _writeRepository.UpdateQuantityAsync(command.Quantity, command.Id);
        }
    }
}