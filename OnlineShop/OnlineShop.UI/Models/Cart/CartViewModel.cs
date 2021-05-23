using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.UI.Models.Cart
{
    public class CartViewModel
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public long ProductId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public decimal? DiscountPrice { get; set; }
        public byte Quantity { get; set; }
        public string ImageUrl { get; set; }
    }
}