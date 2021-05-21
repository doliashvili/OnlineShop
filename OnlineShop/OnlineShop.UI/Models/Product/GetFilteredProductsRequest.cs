using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Domain.Enums;

namespace OnlineShop.UI.Models.Product
{
    public class GetFilteredProductsRequest
    {
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public string? Color { get; set; }
        public string? Brand { get; set; }
        public string? ProductType { get; set; }
        public string? Name { get; set; }
        public Gender? Gender { get; set; }
        public bool? ForBaby { get; set; }
        public string? Size { get; set; }

        [Required]
        public int Page { get; set; }

        [Required]
        public int PageSize { get; set; }
    }
}