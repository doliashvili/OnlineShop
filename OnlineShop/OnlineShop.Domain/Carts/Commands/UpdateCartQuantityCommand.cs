using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreModels.Messaging;

namespace OnlineShop.Domain.Carts.Commands
{
    public class UpdateCartQuantityCommand : Command
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public byte Quantity { get; set; }

        public UpdateCartQuantityCommand(CommandMeta commandMeta, long id, long productId, byte quantity) : base(commandMeta)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}