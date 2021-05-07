using System.Threading.Tasks;
using OnlineShop.Domain.Carts.DomainObjects;

namespace OnlineShop.Domain.AbstractRepository
{
    public interface ICartWriteRepository
    {
        Task AddCartAsync(Cart cart);

        Task RemoveCartAsync(long id);

        Task UpdateCartQuantityAsync(long id, long productId, byte quantity);
    }
}