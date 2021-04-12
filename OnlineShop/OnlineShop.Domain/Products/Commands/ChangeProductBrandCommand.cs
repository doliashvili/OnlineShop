using System.Text.Json.Serialization;
using CoreModels.Messaging;

namespace OnlineShop.Domain.Products.Commands
{
    public class ChangeProductBrandCommand : Command
    {
        public long Id { get; private set; }
        public string Brand { get; private set; }

        [JsonConstructor]
        public ChangeProductBrandCommand(long id, string brand, CommandMeta commandMeta)
            : base(commandMeta)
        {
            Id = id;
            Brand = brand;
        }
    }
}
