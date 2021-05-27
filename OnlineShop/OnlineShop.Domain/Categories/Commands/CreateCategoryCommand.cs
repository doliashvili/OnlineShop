using CoreModels.Messaging;

namespace OnlineShop.Domain.Categories.Commands
{
    public class CreateCategoryCommand : Command
    {
        public string ProductType { get; set; }

        public CreateCategoryCommand(CommandMeta commandMeta, string productType) : base(commandMeta)
        {
            ProductType = productType;
        }
    }
}