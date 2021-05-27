using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreModels.DomainObjects;
using CoreModels.Messaging;
using IdGeneration.GeneratorWrapper;
using OnlineShop.Domain.Categories.Commands;

namespace OnlineShop.Domain.Categories.DomainObjects
{
    public class Category : AggregateRoot
    {
        public override long Id { get; protected set; }
        public string ProductType { get; set; }

        public Category(long id) : base(id)
        {
        }

        public Category(CreateCategoryCommand command) : this(IdGenerator.NewId)
        {
            ProductType = command.ProductType;
        }
    }
}