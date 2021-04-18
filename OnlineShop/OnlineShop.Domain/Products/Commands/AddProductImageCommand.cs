using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreModels.Messaging;
using OnlineShop.Domain.Products.DomainObjects;

namespace OnlineShop.Domain.Products.Commands
{
    public class AddProductImageCommand : Command
    {
        public List<ProductImage> ProductImages { get; }

        public AddProductImageCommand(CommandMeta commandMeta, List<ProductImage> productImages) : base(commandMeta)
        {
            ProductImages = productImages;
        }
    }
}
