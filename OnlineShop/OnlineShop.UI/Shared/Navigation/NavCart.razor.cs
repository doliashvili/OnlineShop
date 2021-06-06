using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.UI.Shared.Navigation
{
    public partial class NavCart
    {
        private int? cartProductsCount = 0;

        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {
        //        var userId = await LocalStorageService.GetItemAsync<string>("userId");
        //        if (userId is null)
        //        {
        //            cartProductsCount = null; //todo null
        //        }
        //        else
        //        {
        //            cartProductsCount = await CartService.GetCartsCountAsync(userId, CancellationToken.None);
        //        }
        //    }
        //}
    }
}