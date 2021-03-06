using System;
using System.Collections.Generic;
using CoreModels.Messaging;
using OnlineShop.Domain.Enums;
using OnlineShop.Domain.Products.DomainObjects;
using OnlineShop.Domain.Products.Records;

namespace OnlineShop.Domain.Products.Commands
{
    public class CreateProductCommand : Command
    {
        public decimal Price { get; }
        public string? Color { get; }
        public string? Brand { get; }
        public string ProductType { get; }
        public Weight? Weight { get; }
        public string? Name { get; }
        public string? Description { get; }
        public Gender? Gender { get; }
        public bool? ForBaby { get; }
        public string? Size { get; }
        public short Quantity { get; }
        public float? Discount { get; }
        public List<ProductImage> Images { get; }
        public DateTime? Expiration { get; }

        public CreateProductCommand(CommandMeta commandMeta,
            decimal price,
            string? color,
            string? brand,
            string productType,
            Weight? weight,
            string? name,
            string? description,
            Gender? gender,
            bool? forBaby,
            string? size,
            short quantity,
            float? discount,
            List<ProductImage> images,
            DateTime? expiration) : base(commandMeta)
        {
            Price = price;
            Color = color;
            Brand = brand;
            ProductType = productType;
            Weight = weight;
            Name = name;
            Description = description;
            Gender = gender;
            ForBaby = forBaby;
            Size = size;
            Quantity = quantity;
            Discount = discount;
            Images = images;
            Expiration = expiration;
        }
    }
}