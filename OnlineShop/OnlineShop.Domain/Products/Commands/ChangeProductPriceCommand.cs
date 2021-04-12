using System.Text.Json.Serialization;
using CoreModels.Messaging;

namespace OnlineShop.Domain.Products.Commands
{
    public class ChangeProductPriceCommand : Command
    {
        public long  Id { get; protected set; }
        public decimal Price { get; private set; }

        [JsonConstructor]
        public ChangeProductPriceCommand(long id, decimal price,CommandMeta commandMeta) 
            : base(commandMeta)
        {
            Id = id;
            Price = price;
        }

    }
}
