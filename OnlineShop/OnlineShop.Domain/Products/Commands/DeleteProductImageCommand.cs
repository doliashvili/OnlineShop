using System.Text.Json.Serialization;
using CoreModels.Messaging;

namespace OnlineShop.Domain.Products.Commands
{
    public class DeleteProductImageCommand : Command
    {
        public long Id { get; private set; }
        public long ImageId { get; private set; }

        [JsonConstructor]
        public DeleteProductImageCommand(long id, long imageId, CommandMeta commandMeta)
            : base(commandMeta)
        {
            Id = id;
            ImageId = imageId;
        }
    }
}
