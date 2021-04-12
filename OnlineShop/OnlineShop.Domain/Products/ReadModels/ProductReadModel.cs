using System;
using System.Collections.Generic;
using OnlineShop.Domain.Enums;
using OnlineShop.Domain.Products.Records;

namespace OnlineShop.Domain.Products.ReadModels
{
    public class ProductReadModel
    {
        public long Id { get; set; }
        public decimal Price { get;  set; }
        public bool? IsDeleted { get;  set; }
        public string? Color { get;  set; }
        public string? Brand { get;  set; }
        public string ProductType { get; set; }
        public Weight? Weight { get;  set; }
        public string? Name { get;  set; }
        public string? Description { get;  set; }
        public Gender? Gender { get;  set; }
        public bool? ForBaby { get;  set; }
        public string? Size { get;  set; }
        public float? Discount { get;  set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? Expiration { get; set; }
        public decimal? DiscountPrice => Discount.HasValue ? Price - (Price * (decimal)Discount.Value) : null;
        public List<ProductReadModelImage> Images { get;  set; }
    }
}
