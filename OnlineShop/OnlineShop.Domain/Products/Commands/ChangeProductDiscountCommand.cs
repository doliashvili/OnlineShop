using System.Text.Json.Serialization;
using CoreModels.Messaging;

namespace OnlineShop.Domain.Products.Commands
{
   public class ChangeProductDiscountCommand : Command
    {
        public long Id { get; protected set; }
        public float Discount { get; private set; }

        [JsonConstructor]
        public ChangeProductDiscountCommand(long id, float discount, CommandMeta commandMeta )
            : base(commandMeta)
        {
            Id = id;
            Discount = discount;
        }

    }
}
