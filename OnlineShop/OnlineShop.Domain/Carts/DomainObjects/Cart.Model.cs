using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreModels.DomainObjects;

namespace OnlineShop.Domain.Carts.DomainObjects
{
    public sealed partial class Cart : AggregateRoot
    {
        public override long Id { get; protected set; }
        public long ProductId { get; private set; }
        public decimal Price { get; private set; }
        public string Name { get; private set; }
        public decimal? DiscountPrice { get; private set; }
        public byte Quantity { get; private set; }
        public string ImageUrl { get; private set; }
    }
}