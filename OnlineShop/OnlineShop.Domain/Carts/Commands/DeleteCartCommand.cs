using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreModels.Messaging;

namespace OnlineShop.Domain.Carts.Commands
{
    public class DeleteCartCommand : Command
    {
        public long Id { get; set; }

        public DeleteCartCommand(CommandMeta commandMeta, long id) : base(commandMeta)
        {
            Id = id;
        }
    }
}