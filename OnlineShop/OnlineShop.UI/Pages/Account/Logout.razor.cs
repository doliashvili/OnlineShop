using System.Threading.Tasks;

namespace OnlineShop.UI.Pages.Account
{
    public partial class Logout
    {
        private async Task SubmitAsync()
        {
            //this.ToastService.ShowSuccess("You have successfully logged out.");
            await _userService.LogOutAsync();
        }
    }
}