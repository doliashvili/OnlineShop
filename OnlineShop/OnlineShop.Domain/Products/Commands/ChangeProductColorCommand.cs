using System.Text.Json.Serialization;
using CoreModels.Messaging;

namespace OnlineShop.Domain.Products.Commands
{
    public class ChangeProductColorCommand : Command
    {
        public long Id { get; protected set; }
        public string Color { get; private set; }

        [JsonConstructor]
        public ChangeProductColorCommand(long id, string color, CommandMeta commandMeta)
            : base(commandMeta)
        {
            Id = id;
            Color = color;
        }
        
    }
}
