using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineShop.UI.Models.User;

namespace OnlineShop.UI.Pages.Account
{
    public partial class ChangePassword
    {
        private readonly ChangePasswordRequest model = new();

        public bool ShowErrors { get; set; }

        public IEnumerable<string> Errors { get; set; }

        private async Task SubmitAsync()
        {
            var response = await _userService.ChangePasswordAsync(model);

            if (response.Succeeded)
            {
                this.ShowErrors = false;

                this.model.OldPassword = null;
                this.model.NewPassword = null;
                this.model.ConfirmNewPassword = null;

                await _userService.LogOutAsync();

                _toastService.ShowSuccess("თქვენი პაროლი წარმატებით შეიცვალა გთხოვთ შეხვიდეთ ახალი პაროლით.");
                _navigationManager.NavigateTo("/account/login");
            }
            else
            {
                this.Errors = response.Errors;
                this.ShowErrors = true;
            }
        }
    }
}