using System.Text.Json.Serialization;
using CoreModels.Messaging;

namespace OnlineShop.Domain.Products.Commands
{
    public class DeleteProductImageCommand : Command
    {
        public long ImageId { get; private set; }

        [JsonConstructor]
        public DeleteProductImageCommand(long imageId, CommandMeta commandMeta)
            : base(commandMeta)
        {
            ImageId = imageId;
        }
    }
}