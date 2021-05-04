using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdGeneration.GeneratorWrapper;

namespace OnlineShop.Domain.Carts.DomainObjects
{
    public sealed partial class Cart
    {
        public Cart(long id) : base(id)
        {
        }

        public Cart(long id,
            long productId,
            decimal price,
            string name,
            decimal? discountPrice,
            byte quantity,
            string imageUrl) : this(IdGenerator.NewId)
        {
            ProductId = productId;
            Price = price;
            Name = name;
            DiscountPrice = discountPrice;
            Quantity = quantity;
            ImageUrl = imageUrl;
        }
    }
}