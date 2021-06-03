using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.UI.Enums;

namespace OnlineShop.UI.Models.Product
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public string? Color { get; set; }
        public string? Brand { get; set; }
        public string ProductType { get; set; }
        public Weight? Weight { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Gender? Gender { get; set; }
        public bool? ForBaby { get; set; }
        public string? Size { get; set; }
        public byte Quantity { get; set; }
        public float? Discount { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? Expiration { get; set; }
        public decimal? DiscountPrice { get; set; }
        public List<Image> Images { get; set; }
    }

    public record Image(long Id, string Url, bool IsMainImage);
    public record Weight(WeightType WeightType, float Value);
}