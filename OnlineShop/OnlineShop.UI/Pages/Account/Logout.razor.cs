using System.Threading.Tasks;

namespace OnlineShop.UI.Pages.Account
{
    public partial class Logout
    {
        private async Task SubmitAsync()
        {
            var name = await _localStorageService.GetItemAsync<string>("firstName");
            _toastService.ShowSuccess($"ნახვამდის ძვირფასო {name}");
            await _userService.LogOutAsync();
        }
    }
}