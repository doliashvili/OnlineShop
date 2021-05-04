using CoreModels.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Products.Commands
{
    public class ChangeProductQuantityCommand : Command
    {
        public long Id { get; private set; }
        public byte Quantity { get; private set; }

        [JsonConstructor]
        public ChangeProductQuantityCommand(long id, byte quantity, CommandMeta commandMeta)
            : base(commandMeta)
        {
            Id = id;
            Quantity = quantity;
        }
    }
}