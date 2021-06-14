using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.UI.Models.Cart
{
    public class UpdateCartRequest
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public short Quantity { get; set; }
    }
}