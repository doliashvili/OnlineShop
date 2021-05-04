using System.Collections.Generic;

namespace OnlineShop.Domain.CommonModels.CartsGuests
{
    public class CartGuest
    {
        public string Ip { get; set; }

        public HashSet<CartGuestItem> CartItems { get; set; }
    }
}