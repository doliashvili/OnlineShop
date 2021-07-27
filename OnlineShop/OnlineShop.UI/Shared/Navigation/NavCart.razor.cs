using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.UI.Shared.Navigation
{
    public partial class NavCart
    {
        private int? cartProductsCount;

        protected override async Task OnInitializedAsync()
        {
            var userId = await _localStorageService.GetItemAsync<string>("userId");
            if (userId is null)
            {
                cartProductsCount = 0;
            }
            else
            {
                cartProductsCount = await _cartService.GetCartsCountAsync(userId, CancellationToken.None);
            }
        }
    }
}