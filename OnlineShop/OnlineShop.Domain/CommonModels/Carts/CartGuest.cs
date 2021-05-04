using System.Collections.Generic;

namespace OnlineShop.Domain.CommonModels.Carts
{
    public class CartGuest
    {
        public string Ip { get; set; }

        public HashSet<CartGuestItem> CartItems { get; set; }
    }
}