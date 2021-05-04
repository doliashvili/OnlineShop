using System;
using System.Collections.Generic;
using CoreModels.DomainObjects;
using OnlineShop.Domain.Enums;
using OnlineShop.Domain.Products.Records;

namespace OnlineShop.Domain.Products.DomainObjects
{
    public partial class Product : AggregateRoot
    {
        public override long Id { get; protected set; }
        public decimal Price { get; private set; }
        public bool? IsDeleted { get; private set; }
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
        public byte Quantity { get; set; }
        public decimal? DiscountPrice => Discount.HasValue ? Price - (Price * (decimal)Discount.Value) : null;
        public DateTime? CreateTime { get; private set; }
        public DateTime? Expiration { get; private set; }
        public List<ProductImage> Images { get; private set; }
    }
}