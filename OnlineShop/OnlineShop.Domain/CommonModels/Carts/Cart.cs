using System.Collections.Generic;

namespace OnlineShop.Domain.CommonModels.Carts
{
    public class Cart
    {
        public string Ip { get; set; }

        public HashSet<CartItem> CartItems { get; set; }
    }
}