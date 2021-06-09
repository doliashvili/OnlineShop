using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OnlineShop.UI.Models.User;

namespace OnlineShop.UI.Pages.Account
{
    public partial class ForgetPassword
    {
        private ForgotPasswordRequest model = new();

        private bool ShowErrors { get; set; }

        private IEnumerable<string> Errors { get; set; }

        private async Task SubmitAsync()
        {
            var result = await _userService.ForgetPasswordAsync(model);
            if (result.Succeeded)
            {
                ShowErrors = false;
                _toastService.ShowSuccess("თქვენ გამოგეგზავნათ მეილზე განახლებადი კოდი"); //todo /translate
                _navigationManager.NavigateTo("/account/resetPassword");
            }
            else
            {
                Errors = result.Errors;
                ShowErrors = true;
            }
        }
    }
}