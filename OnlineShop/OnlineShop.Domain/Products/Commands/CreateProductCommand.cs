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
        public float? Discount { get; }
        public DateTime? CreateTime { get; }
        public List<ProductImage> Images { get; }
        public DateTime? Expiration { get; }

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
            CommandMeta commandMeta) : base(commandMeta)
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
