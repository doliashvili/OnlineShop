using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreModels.Messaging;

namespace OnlineShop.Domain.Categories.Commands
{
    public class DeleteCategoryCommand : Command
    {
        public long Id { get; set; }

        public DeleteCategoryCommand(CommandMeta commandMeta, long id) : base(commandMeta)
        {
            Id = id;
        }
    }
}