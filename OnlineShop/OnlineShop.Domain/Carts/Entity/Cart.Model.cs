using CoreModels.DomainObjects;

namespace OnlineShop.Domain.Carts.DomainObjects
{
    public sealed partial class Cart : Entity
    {
        public override long Id { get; protected set; }
        public long ProductId { get; private set; }
        public string UserId { get; private set; }
        public byte Quantity { get; private set; }
    }
}