using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineShop.Domain.Products.DomainObjects;

namespace OnlineShop.Domain.AbstractRepository
{
    public interface IProductWriteRepository
    {
        public Task CreateAsync(Product product);

        public Task AddImagesAsync(List<ProductImage> images);

        public Task DeleteImageAsync(long imageId);

        public Task DeleteProductAsync(long id);

        public Task UpdateNameAsync(string name, long id);

        public Task UpdateQuantityAsync(byte quantity, long id);

        public Task UpdatePriceAsync(decimal price, long id);

        public Task UpdateDiscountAsync(float discount, long id);

        public Task UpdateBrandAsync(string brand, long id);

        public Task UpdateTypeAsync(string type, long id);

        public Task UpdateForBabyAsync(bool isBaby, long id);

        public Task UpdateColorAsync(string color, long id);
    }
}