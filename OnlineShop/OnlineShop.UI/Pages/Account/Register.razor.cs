using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineShop.UI.Models.User;

namespace OnlineShop.UI.Pages.Account
{
    public partial class Register
    {
        private readonly RegisterRequest model = new RegisterRequest();

        public bool ShowErrors { get; set; }

        public IEnumerable<string> Errors { get; set; }

        private async Task SubmitAsync()
        {
            var result = await _userService.RegistrationAsync(model);

            if (result.Succeeded)
            {
                this.ShowErrors = false;

                //this.ToastService.ShowSuccess(
                //    "You have successfully registered.\n Please login.",
                //    "Congratulations!");

                _navigationManager.NavigateTo("/account/login");
            }
            else
            {
                // this.Errors = result.Errors;
                this.ShowErrors = true;
            }
        }
    }
}