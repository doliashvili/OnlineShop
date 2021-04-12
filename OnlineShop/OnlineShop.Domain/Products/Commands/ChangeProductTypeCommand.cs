using System.Text.Json.Serialization;
using CoreModels.Messaging;

namespace OnlineShop.Domain.Products.Commands
{
    public class ChangeProductTypeCommand : Command
    {
        public  long Id { get; protected set; }
        public string ProductType { get; private set; }

        [JsonConstructor]
        public ChangeProductTypeCommand(long id, string productType,CommandMeta commandMeta)
        : base(commandMeta)
        {
            Id = id;
            ProductType = productType;
        }
    }
}
