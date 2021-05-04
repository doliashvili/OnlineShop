using IdGeneration.GeneratorWrapper;
using OnlineShop.Domain.Products.Commands;

namespace OnlineShop.Domain.Products.DomainObjects
{
    public sealed partial class Product
    {
        public Product(long id) : base(id)
        {
        }

        public Product(CreateProductCommand command) : this(IdGenerator.NewId)
        {
            Price = command.Price;
            Color = command.Color;
            Brand = command.Brand;
            ProductType = command.ProductType;
            Weight = command.Weight;
            Name = command.Name;
            Description = command.Description;
            Gender = command.Gender;
            ForBaby = command.ForBaby;
            Size = command.Size;
            Quantity = command.Quantity;
            Discount = command.Discount;
            CreateTime = command.CreateTime;
            Expiration = command.Expiration;
            Images = command.Images;
        }
    }
}