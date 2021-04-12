using System.Text.Json.Serialization;
using CoreModels.Messaging;

namespace OnlineShop.Domain.Products.Commands
{
    public class DeleteProductCommand : Command
    {
        public long Id { get; private set; }

        [JsonConstructor]
        public DeleteProductCommand(long id, CommandMeta commandMeta)
            : base(commandMeta)
        {
            Id = id;
        }
    }
}
