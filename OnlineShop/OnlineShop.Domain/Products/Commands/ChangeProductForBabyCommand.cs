using System.Text.Json.Serialization;
using CoreModels.Messaging;

namespace OnlineShop.Domain.Products.Commands
{
    public class ChangeProductForBabyCommand : Command
    {
        public long Id { get; private set; }
        public bool ForBaby { get; private set; }

        [JsonConstructor]
        public ChangeProductForBabyCommand(long id, bool forBaby, CommandMeta commandMeta)
        : base(commandMeta)
        {
            Id = id;
            ForBaby = forBaby;
        }
    }
}
