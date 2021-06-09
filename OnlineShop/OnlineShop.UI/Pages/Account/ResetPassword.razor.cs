using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.UI.Models.User;

namespace OnlineShop.UI.Pages.Account
{
    public partial class ResetPassword
    {
        private ResetPasswordRequest model = new();
        public bool ShowErrors { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public async Task SubmitAsync()
        {
            var result = await _userService.ResetPasswordAsync(model);

            if (result.Succeeded)
            {
                ShowErrors = false;
                _toastService.ShowSuccess("თქვენი პაროლი წარმატებით განახლდა"); //todo /translate
                _navigationManager.NavigateTo("/account/login");
            }
            else
            {
                Errors = result.Errors;
                ShowErrors = true;
            }
        }
    }
}