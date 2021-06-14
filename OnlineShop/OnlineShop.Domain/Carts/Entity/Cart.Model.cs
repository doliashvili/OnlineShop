namespace OnlineShop.Domain.Carts.Entity
{
    public sealed partial class Cart : CoreModels.DomainObjects.Entity
    {
        public override long Id { get; protected set; }
        public long ProductId { get; private set; }
        public string UserId { get; private set; }
        public short Quantity { get; private set; }
    }
}