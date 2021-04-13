using System.Text.Json.Serialization;
using CoreModels.Messaging;
using OnlineShop.Domain.Enums;
using OnlineShop.Domain.Products.ReadModels;

namespace OnlineShop.Domain.Products.Queries
{
    public class GetFilteredProducts : IQuery<PagingProductModel>
    {
        [JsonConstructor]
        public GetFilteredProducts(decimal? priceFrom,
            decimal? priceTo,
            string color,
            string brand,
            string productType,
            string name,
            Gender? gender,
            bool? forBaby,
            string size,
            int page,
            int pageSize)
        {
            PriceFrom = priceFrom ?? decimal.MinValue;
            PriceTo = priceTo ?? decimal.MaxValue;
            Color = color;
            Brand = brand;
            ProductType = productType;
            Name = name;
            Gender = gender;
            ForBaby = forBaby;
            Size = size;
            Page = page;
            PageSize = pageSize;
        }

        public GetFilteredProducts()
        {

        }

        public decimal? PriceFrom { get; set; } = decimal.MinValue;
        public decimal? PriceTo { get; set; } = decimal.MaxValue;
        public string Color { get; set; }
        public string Brand { get; set; }
        public string ProductType { get; set; }
        public string Name { get; set; }
        public Gender? Gender { get; set; }
        public bool? ForBaby { get; set; }
        public string Size { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
