using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CoreModels.Messaging;
using OnlineShop.Domain.Enums;
using OnlineShop.Domain.Products.DomainObjects;
using OnlineShop.Domain.Products.Records;

namespace OnlineShop.Domain.Products.Commands
{
    public class CreateProductCommand : Command
    {
        public decimal Price { get; private set; }
        public string? Color { get; private set; }
        public string? Brand { get; private set; }
        public string ProductType { get; set; }
        public Weight? Weight { get; private set; }
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public Gender? Gender { get; private set; }
        public bool? ForBaby { get; private set; }
        public string? Size { get; private set; }
        public float? Discount { get; private set; }
        public DateTime? CreateTime { get; private set; }
        public List<ProductImage> Images { get; private set; }
        public DateTime? Expiration { get; private set; }

        [JsonConstructor]
        public CreateProductCommand(
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
            float? discount,
            DateTime? createTime,
            List<ProductImage> images,
            DateTime expiration,
            CommandMeta commandMeta ) : base(commandMeta)
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
            Discount = discount;
            CreateTime = createTime;
            Images = images;
            Expiration = expiration;
        }

    }
}
