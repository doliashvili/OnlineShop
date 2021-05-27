using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cqrs;
using Cqrs.ApiGenerator;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.Categories.Commands;
using OnlineShop.Domain.Categories.DomainObjects;

namespace OnlineShop.Application.CommandHandlers
{
    [ApiGen]
    public class CategoryCommandHandler : ICommandHandler<CreateCategoryCommand>,
        ICommandHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryWriteRepository _writeRepository;

        public CategoryCommandHandler(ICategoryWriteRepository writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public Task HandleAsync(CreateCategoryCommand command)
        {
            return _writeRepository.AddCategoryAsync(new Category(command));
        }

        public Task HandleAsync(DeleteCategoryCommand command)
        {
            return _writeRepository.DeleteCategoryAsync(command.Id);
        }
    }
}