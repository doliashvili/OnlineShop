using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CoreModels.Messaging;

namespace OnlineShop.Domain.Products.Commands
{
    public class ChangeProductNameCommand : Command
    {
        public long Id { get; protected set; }
        [Required]
        public string Name { get; private set; }

        [JsonConstructor]
        public ChangeProductNameCommand(long id,string name,CommandMeta commandMeta)
            : base(commandMeta)
        {
            Id = id;
            Name = name;
        }
    }
}
