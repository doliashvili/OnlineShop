using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.UI.Models.Cart
{
    public class AddCartRequest
    {
        public long ProductId { get; set; }
        public byte Quantity { get; set; }
        public string UserId { get; set; }
    }
}