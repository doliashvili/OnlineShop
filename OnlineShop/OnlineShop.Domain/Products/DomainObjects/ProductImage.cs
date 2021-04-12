using CoreModels.DomainObjects;

namespace OnlineShop.Domain.Products.DomainObjects
{
    public class ProductImage : Entity
    {
        public override long Id { get; protected set; }
        public string Url { get; set; }
        public bool MainImage { get; set; }
        public long? ProductId { get; set; }
    }
}
