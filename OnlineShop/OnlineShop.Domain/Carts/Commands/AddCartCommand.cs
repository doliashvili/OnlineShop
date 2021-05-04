using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreModels.Messaging;

namespace OnlineShop.Domain.Carts.Commands
{
    public sealed class AddCartCommand : Command
    {
        public long ProductId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public decimal? DiscountPrice { get; set; }
        public byte Quantity { get; set; }
        public string ImageUrl { get; set; }
        public string UserId { get; set; }

        public AddCartCommand(CommandMeta commandMeta, long productId, decimal price, string name,
            decimal? discountPrice, byte quantity, string imageUrl, string userId) : base(commandMeta)
        {
            ProductId = productId;
            Price = price;
            Name = name;
            DiscountPrice = discountPrice;
            Quantity = quantity;
            ImageUrl = imageUrl;
            UserId = userId;
        }
    }
}