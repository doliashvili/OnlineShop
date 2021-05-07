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
        public byte Quantity { get; set; }
        public string UserId { get; set; }

        public AddCartCommand(CommandMeta commandMeta, long productId, byte quantity, string userId) : base(commandMeta)
        {
            ProductId = productId;
            Quantity = quantity;
            UserId = userId;
        }
    }
}