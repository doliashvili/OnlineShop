using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineShop.UI.Models.User;

namespace OnlineShop.UI.Pages.Account
{
    public partial class Login
    {
        private readonly LoginRequest model = new LoginRequest();

        public bool ShowErrors { get; set; }

        public IEnumerable<string> Errors { get; set; }

        private async Task SubmitAsync()
        {
            var result = await _userService.LoginAsync(model);

            if (result.Result.Succeeded)
            {
                ShowErrors = false;
                //this.ToastService.ShowSuccess("You have successfully logged in");
                _navigationManager.NavigateTo("/");
            }
            else
            {
                Errors = new List<string>() { "invalid error" }; //todo
                this.ShowErrors = true;
            }
        }
    }
}