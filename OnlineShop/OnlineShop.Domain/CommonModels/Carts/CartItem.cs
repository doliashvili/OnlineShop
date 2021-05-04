using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.CommonModels.Carts
{
    public record CartItem
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public byte Quantity { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}