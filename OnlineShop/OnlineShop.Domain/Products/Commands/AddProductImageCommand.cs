using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreModels.Messaging;

namespace OnlineShop.Domain.Products.Commands
{
    public class AddProductImageCommand : Command
    {
        public AddProductImageCommand(CommandMeta commandMeta) : base(commandMeta)
        {
        }
    }
}
